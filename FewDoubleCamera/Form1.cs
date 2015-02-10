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
using System.Speech.Synthesis;
using System.Diagnostics;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace FewDoubleCamera
{
    public partial class FormDuble : Form
    {
        String n;
        private Capture capture;
        private bool progres;
        private HaarCascade haar; // Detector menggunakan Viola=John clasifier
        int TotalFaces,tf;
        bool sudah = false;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result = null, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;
        SpeechSynthesizer synthesizer;
         //bool sudah = false;
        private IModel channel;
        private Subscription sub;
        long receivedImageCount = 0;

        public FormDuble()
        {
            InitializeComponent();
            this.Text = "Pengenalan Wajah";
            synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private List<HumanFaceRecognized> processFrame(Image<Bgr, byte> capturedFrame)
        {
            List<HumanFaceRecognized> recognizeds = new List<HumanFaceRecognized>();
            Image<Bgr, Byte> imageFrame = capturedFrame.Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            if (imageFrame != null)
            {
                ib2.Image = imageFrame;

                iB.Image = imageFrame;
                Image<Gray, byte> grayframe = imageFrame.Convert<Gray, byte>();
                var faces = grayframe.DetectHaarCascade(haar, 1.1, 1,
                                        HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                                        new Size(20, 20))[0];
                TotalFaces = faces.Length;   
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
                        HumanFaceRecognized recognized = new HumanFaceRecognized(name, new Vector3(face.rect.X, face.rect.Y, 0), new Vector3(face.rect.Right, face.rect.Bottom, 0));
                        recognizeds.Add(recognized);
                    }
                }
                  //synthesizer.SpeakAsync(n + ", i see you with some one");
                 if (tf != TotalFaces) sudah = false;
                   n = "Hello " + name;
                   //synthesizer = new SpeechSynthesizer(); 
                   if (TotalFaces > 1 && TotalFaces != 0 && sudah!=true)
                   {
                      // synthesizer = new SpeechSynthesizer(); 
                       tf=TotalFaces;
                       synthesizer.SpeakAsync(n + ", i see you with some one");
                       sudah = true;
                 
                   }
                   else if (TotalFaces == 1 && sudah!=true)
                   {
                       // synthesizer=new SpeechSynthesizer();
                       tf = TotalFaces;
                       synthesizer.SpeakAsync(n + ", i see you Alone");
                       sudah = true;
                   }
                   else if ( sudah!=true)
                   {
                        //synthesizer = new SpeechSynthesizer(); 
                       tf = TotalFaces;
                       synthesizer.SpeakAsync("i do not see any one");
                       sudah = true;
                   }
                  
                  //Clear the list(vector) of names
                  NamePersons.Clear();
            }
            return recognizeds;
        }

        private void prossesFrame(object sender, EventArgs args)
        {
            Image<Bgr, byte> capturedFrame = capture.QueryFrame();
            processFrame(capturedFrame);
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
                    Debug.WriteLine("Added face '{0}' from '{1}'", Labels[no], filewajah);
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

        private void FormDuble_MouseClick(object sender, MouseEventArgs e)
        {
            sudah = false;
        }

        private void activateMessagingBtn_Click(object sender, EventArgs e)
        {
            ambilData();

            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = "amqp://lumen:lumen@167.205.66.130/%2F";
            Debug.WriteLine("Connecting to AMQP ...");
            IConnection conn = factory.CreateConnection();
            channel = conn.CreateModel();
            conn.AutoClose = true;
            Debug.WriteLine("Connected to AMQP broker '{0}:{1}'", conn.RemoteEndPoint, conn.RemotePort);

            var arg = new Dictionary<string, object>
            {
                //{"x-message-ttl", 50}
            };
            QueueDeclareOk cameraStream = channel.QueueDeclare("", false, true, true, arg);
            Debug.WriteLine("Declared anonymous exclusive queue '{0}'", (object) cameraStream.QueueName);
            string cameraStreamKey = "avatar.NAO.data.image";
            channel.QueueBind(cameraStream.QueueName, "amq.topic", cameraStreamKey);
            Debug.WriteLine("Bound queue '{0}' to topic '{1}'", cameraStream.QueueName, cameraStreamKey);
            
            sub = new Subscription(channel, cameraStream.QueueName,true);
            messagingTimer.Enabled = true;
            messagingTimer.Interval = 10;
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
            //Debug.WriteLine("messaging timer in");
            string lastMsg = null;

            /*
            foreach (BasicDeliverEventArgs ev in sub)
            {
                Debug.WriteLine("while loop");
                try
                {
                    string bodyStr = Encoding.UTF8.GetString(ev.Body);
                    Debug.WriteLine("Got message: {0}", (object) bodyStr);
                    receivedImageCount++;
                    lastMsg = bodyStr;
                }
                finally
                {
                    sub.Ack(ev);
                }
            }
             * */
            BasicDeliverEventArgs ev;
            while (sub.Next(1, out ev))
            {
                Debug.WriteLine("while loop");
                try
                {
                    string bodyStr = Encoding.UTF8.GetString(ev.Body);
                    Debug.WriteLine("Got message: {0}", bodyStr);
                    receivedImageCount++;
                    lastMsg = bodyStr;
                }
                finally
                {
                    sub.Ack(ev);
                }
            }

            if (lastMsg != null) {
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
                ImageObject imageObj = JsonConvert.DeserializeObject<ImageObject>(lastMsg, jsonSettings);
                Debug.WriteLine("Got object after {0} images: {1}", receivedImageCount, imageObj);
                string base64 = null;
                if (imageObj.ContentUrl.StartsWith("data:image/jpeg;base64,"))
                {
                    base64 = imageObj.ContentUrl.Replace("data:image/jpeg;base64,", "");
                }
                if (imageObj.ContentUrl.StartsWith("data:image/png;base64,"))
                {
                    base64 = imageObj.ContentUrl.Replace("data:image/png;base64,", "");
                }
                if (imageObj.ContentUrl.StartsWith("data:image/bmp;base64,"))
                {
                    base64 = imageObj.ContentUrl.Replace("data:image/bmp;base64,", "");
                }
                if (imageObj.ContentUrl.StartsWith("data:image/gif;base64,"))
                {
                    base64 = imageObj.ContentUrl.Replace("data:image/gif;base64,", "");
                }
                if (base64 != null)
                {
                    byte[] bytes = Convert.FromBase64String(base64);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        Bitmap bmp = (Bitmap)Image.FromStream(ms);
                        Image<Bgr, byte> receivedImage = new Image<Bgr, byte>(bmp);
                        List<HumanFaceRecognized> recognizeds = processFrame(receivedImage);
                        Debug.WriteLine("Recognized {0} faces: {1}", recognizeds.Count, recognizeds);
                        const string humanRecognitionKey = "lumen.arkan.face.recognition";
                        foreach (HumanFaceRecognized recognized in recognizeds)
                        {
                            string recognizedStr = JsonConvert.SerializeObject(recognized, Formatting.Indented);
                            Debug.WriteLine("Sending to {0}: {1}", humanRecognitionKey, recognizedStr);
                            byte[] recognizedBytes = Encoding.UTF8.GetBytes(recognizedStr);
                            channel.BasicPublish("amq.topic", humanRecognitionKey, null, recognizedBytes);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Unsupported content URI: " + imageObj.ContentUrl);
                }
            }            
            else
            {
                Debug.WriteLine("No incoming ImageObject message after {0} images", receivedImageCount);
            }
            //Debug.WriteLine("messaging timer out");
        }

        private void FormDuble_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (synthesizer != null)
            {
                synthesizer.Dispose();
                synthesizer = null;
            }
            if (sub != null)
            {
                stopMessagingBtn.PerformClick();
            }
        }
    }
}
