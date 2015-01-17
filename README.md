# FewDoubleCamera
Human Face Recognition

## How to Build and Run

1. Install [Git](http://git-scm.com). Configure `http.proxy` if needed.
2. Checkout this repository to e.g. `C:\Users\me\git\FewDoubleCamera`
3. Install [Visual C# 2010 Express](http://www.visualstudio.com/en-us/downloads#d-2010-express) or newer.

Hendy's note: While Visual Studio 2012 and newer includes NuGet, for VS2010, only [NuGet command line](http://docs.nuget.org/docs/start-here/installing-nuget) (must be added to `PATH`) is supported. And even then, it won't add references automatically and doesn't seem to support `packages.config`, so I guess it's better to just embed the dependencies until we require VS2012+.

## How to Setup Messaging

If you plan to enable the messaging system, you need to setup this.

1. Install [Erlang OTP](http://www.erlang.org/download.html)
2. Install [RabbitMQ for Windows](https://www.rabbitmq.com/install-windows.html)
