using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using Newtonsoft.Json;

namespace FewDoubleCamera
{
    public partial class FormDuble : Form
    {
        private Capture capture;
        private bool progres;
        private HaarCascade haar; // Detector menggunakan Viola=John clasifier
       

        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result = null, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;
        private IModel channel;
        private Subscription sub;

        public FormDuble()
        {
            InitializeComponent();
            this.Text = "Pengenalan Wajah";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void prossesFrame(object sender, EventArgs args)
        {

            Image<Bgr, Byte> imageFrame = capture.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC); 
            if (imageFrame != null)
            {
                Image<Gray, byte> grayframe = imageFrame.Convert<Gray, byte>();
                var faces = grayframe.DetectHaarCascade(haar, 1.1, 1,
                                        HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                                        new Size(20, 20))[0];
                int TotalFaces = faces.Length;   
                lt.Text = Convert.ToString(TotalFaces);
              

                foreach (var face in faces)
                {
                    t = t + 1;
                    result = imageFrame.Copy(face.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    imageFrame.Draw(face.rect, new Bgr(Color.Red), 2);
                     if (trainingImages.ToArray().Length != 0)
                    {
                        MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                        trainingImages.ToArray(),
                        labels.ToArray(),
                        3000,
                        ref termCrit);
                        name = recognizer.Recognize(result);             
                        imageFrame.Draw(name, ref font, new Point(face.rect.X - 2, face.rect.Y - 2), new Bgr(Color.Yellow));
                    }
                   // NamePersons[t-1] = name;
                   // NamePersons.Add("");
                    //Set the number of faces detected on the scene
                    //label3.Text = faces.Length.ToString();
                }
                /*t = 0;
                for (int nnn = 0; nnn < faces.Length; nnn++)
                {
                        names = names + NamePersons[nnn] + ", ";
                }*/
                   iB.Image = imageFrame;
                  // names = "";
                    //Clear the list(vector) of names
                  NamePersons.Clear();
            }
        }



        private void bStart_Click(object sender, EventArgs e)
        {
            ambilData();
            int number = 1;
            number = int.Parse(cBKamera.Text);
            #region
            if (capture == null)
            {
                try
                {
                    capture = new Capture(number);
                }
                catch (NullReferenceException exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            #endregion
            if (capture != null)
            {
                if (progres)
                {
                    bStart.Text = "Start Deteksi";
                    Application.Idle -= prossesFrame;

                }
                else
                {
                    bStart.Text = "Stop";
                    Application.Idle += prossesFrame;
                }
                progres = !progres;
                
            }
        }

        private void FormDuble_Load(object sender, EventArgs e)
        {
            haar = new HaarCascade("haarcascade_frontalface_alt_tree.xml");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAmbil_Click(object sender, EventArgs e)
        {
            

            try
            {
               
                ContTrain = ContTrain + 1;
                gray = capture.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                Image<Gray, byte> grayframe = gray.Convert<Gray, byte>();
                var faces = grayframe.DetectHaarCascade(haar, 1.1, 1,
                                        HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                                        new Size(20, 20))[0];
                
                foreach (var face in faces)
                {
                    TrainedFace = gray.Copy(face.rect).Convert<Gray, byte>();
                    break;
                }
                TrainedFace=result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                imageBox1.Size = TrainedFace.Size;
                imageBox1.Image = TrainedFace;
            }
            catch
            {
                //Jika terjadi kesalahan
            }
            //trainingImages.Add(TrainedFace);
            imageBox1.Image = TrainedFace;
        }


        private void ambilData()
        {
            try
            {
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/wajah/namaTrain.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string filewajah;

                for (int no = 1; no < NumLabels + 1; no++)
                {
                    filewajah = "wajah" + no + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/wajah/" + filewajah));
                    labels.Add(Labels[no]);
                }
            }
            catch 
            {
                MessageBox.Show("Data Pelatihan tidak ditemukan"+"\n"+"Lajutkan dengan pembuatan data pelatihan", "Load Wajah Pelatihan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text == "")
                MessageBox.Show("Nama Pemilik Gambar Harus Terisi", "Data Pelatihan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
               // TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(textBoxNama.Text);

                try{
                    File.WriteAllText(Application.StartupPath + "/wajah/namaTrain.txt", trainingImages.ToArray().Length.ToString() + "%");
                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/wajah/wajah" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/wajah/namaTrain.txt", labels.ToArray()[i - 1] + "%");
                }

                MessageBox.Show("Wajah "+textBoxNama.Text + " telah dideteksi dan ditambahkan pada Data Training", "Proses Data Training ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Keslahan Proses Training", "Kesalahan Training", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            }
        }

        private void activateMessagingBtn_Click(object sender, EventArgs e)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = "amqp://guest:guest@localhost/%2F";
            IConnection conn = factory.CreateConnection();
            channel = conn.CreateModel();
            conn.AutoClose = true;
            Debug.WriteLine("Connected to AMQP broker '{0}:{1}'", conn.RemoteEndPoint, conn.RemotePort);

            QueueDeclareOk cameraStream = channel.QueueDeclare("", false, true, true, null);
            Debug.WriteLine("Declared anonymous exclusive queue '{0}'", (object) cameraStream.QueueName);
            string cameraStreamKey = "lumen.arkan.camera.stream";
            channel.QueueBind(cameraStream.QueueName, "amq.topic", cameraStreamKey);
            Debug.WriteLine("Bound queue '{0}' to topic '{1}'", cameraStream.QueueName, cameraStreamKey);
            sub = new Subscription(channel, cameraStream.QueueName);
            messagingTimer.Enabled = true;
        }

        private void stopMessagingBtn_Click(object sender, EventArgs e)
        {
            messagingTimer.Enabled = false;
            sub.Close();
            sub = null;
            channel.Close();
            channel = null;
        }

        private void messagingTimer_Tick(object sender, EventArgs e)
        {
            BasicDeliverEventArgs ev;
            if (sub.Next(1, out ev))
            {
                string bodyStr = Encoding.UTF8.GetString(ev.Body);
                Debug.WriteLine("Got message: {0}", bodyStr);
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
                ImageObject imageObj = JsonConvert.DeserializeObject<ImageObject>(bodyStr, jsonSettings);
                Debug.WriteLine("Got object: {0}", imageObj);
                sub.Ack(ev);
            }
            else
            {
                Debug.WriteLine("No incoming ImageObject message");
            }
        }
    }
}
