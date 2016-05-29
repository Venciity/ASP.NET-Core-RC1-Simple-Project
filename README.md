# ASP.NET Core RC1 simple project
The objectives of this project are to show you how to use ASP.NET Core RC1.
I follow the steps to write this project from the [Plularsight course](https://www.pluralsight.com/courses/aspdotnet-core-1-0-fundamentals)

## 1.Build your first ASP.NET Core application
### 1.1 Setup
* First you need to install any version of Visual Studio. You can use Microsoft DreamSpark if you're a student and your university give you that chance. If you aren't a student or don't have a Microsoft DreamSpark account you can download free version of Visual Studio (Visual Studio Community 2015) from [here](https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx)
* Also you need to install the ASP.NET Core from here(https://docs.asp.net/en/latest/getting-started.html). Just follow the installation steps and you'll get it.

### 1.2 Create a new project
* You can create a new project by going to file :arrow_right: new project :arrow_right: Visual C# :arrow_right: ASP.NET Web Application. Give it a name, select the location of the project and then click "OK". At the top, they're ASP.NET 4.5.2 Templates, after that they're ASP.NET 5 Templates. You can select an empty project from the ASP.NET 5 Templates, maybe remove the tick "Host in cloud" and
click "OK".

* We can start the project without debugging with shortcut ctrl + f5. By default when we create an empty project and then we start it, we see the text "Hello world!".

### 1.3 The project layout
* The "projects" setting in global.json tells to ASP.NET where to look for my source code and what folders contain my projects. They're two possible folders: src for source and a test folder. If my projects aren't in one of these two folders, the code won't be available to build.

* When we open the project folder in file explorer, then double click on src folder and then open the folder with the project name, we will view the source code files for the application.

* In this version of ASP.NET, the file system determines what is in my project. If we add a new file in the source code folder, the file will be added to the project. Also, if we delete a file, the file is removed from the project.
That's a little bit different than previous versions of ASP.NET where a project file (a .csproj file) that contained a manifest of everything that is in the project. Here the file system is the project system and that's important to understand in this version of ASP.NET.

* ASP.NET will compile the application when we make file changes, add a file or delete a file. ASP.NET monitor the file system and recompile the application on file changes. We don't need explicitly build the application in Visual Studio.

### 1.4 The project.json file
* The project.json file is a file using JSON (Javascript Object Notation) to store configuration information. This file is a heart of a .NET application.

* If my application is going to do any useful work, I will go to install some libraries and frameworks to do that work. Work like render a complicated HTML or talk to the database.

* All dependencies are managed via the NuGet package manager. NuGet has been around the .NET space for a few years. Now the primary way to manage dependencies is to use libraries and frameworks that are wrapped as NuGet packages.

* When we need to add a new dependency, like an ASP.NET MVC framework, I can type into the project.json file. I have an intellisense about the packages(the package name and version numbers). Or I can use the UI. I can right-click on References and then click "manage NuGet packages". When we install the package from the UI, that package will be stored in the project.json file.

* The "frameworks" section tells ASP.NET which of the .NET frameworks my application can use. <br /> <br />
Dnx451 is the full .NET framework. This is the .NET framework that is installed when I install Visual Studio. It's the .NET Framework that has been around for 15 years, and it includes frameworks that do everything from web programming to desktop programming. So frameworks like Windows Presentation Foundation and Windows Communication Foundation. It's a huge framework, and it's a framework that only works on Windows. <br /> <br />
Dnxcore50, this is the .NET Core framework. .NET Core is a cross-platform framework so it can work on various platforms, not just Windows but also OS X and Linux. This framework has far fewer features than the full dnx451 framework. But it does have all the features that we need for ASP.NET web development. So, we expect the most ASP.NET applications will only target .NET Core in a few years.

* When we expand the References node in the project. We can see the frameworks which our application using.
When we expand some of this nodes. We can also see the NuGet package that we referencing. We can expand the NuGet packages too and then we can see that the nuget packages have dependacies that are also NuGet packages.

* The folder "wwwrooot" is the web root from hosting perspective, this is the root of the website. If we have files that we want to serve over HTTP, static files that are on the file system, like image files, stylesheets, and Javascript files, we need to place this files into this web root folder to make them available.

* If you're worked with previous versions of ASP.NET, you probably expect to see a **global.asax** file, which was one place where I could write code to execute during startup of a web application. And you'd also probably expect to see a **web.config** file containing all the configuration parameters my application needed to execute. **Those files are gone**. Instead, configuration and startup code are loaded from a Startup.cs file. Inside of this file, there is a Startup class. This is a class that ASP.NET will look for by convention, and there we configure our application and configuration sources.

* The Configure method in the Startup class we build our HTTP processing pipeline. This defines how my application responds to requests. Configure method is a place to set up the inversion of control container for the application.

* The ConfigureServercies method in Startup class is a place to configure components for the application.

* ASP.NET uses dependency injection everywhere to give us a lot of flexability on how an application behaves. Essentially with dependency injection, I can have my code ask for dependencies instead of instantiating dependacies directly and being tied to some specific component.

## 2.Startup and middleware
### 2.1 How middleware works
* When an HTTP request arrives at our serve, and let's pretend we have an HttpPost request to the URL/reviews, in this example, we need software that will respond to this request.In ASP.NET, it is ultimately middleware components that will determine how to process this request.Each piece of middleware in ASP.NET is an object, and each piece has a very specific, very focused, and very limited role. So ultimately we need many pieces of middleware for an application to behave appropriately.

* So let's imagine that we want to log information about every request to our application.
In that case, the first piece of middleware that we might install into the application is a logging component. This logger can see everything about the incoming request: the path, the query string, the headers, any cookies and access tokens and the logger can record information about the request. It can even change information about the request if it wanted to or reject the request and just stop processing right away. But chances are a logger is simply going to record some information and then pass along this request to the next piece of middleware. So middleware is a series of components that are all in this processing pipeline. 

* And let's say that the next piece of middleware that we've installed into the application is an authorizer. An authorizer might be looking for a specific cookie or access tokens in the HTTP headers. If the authorizer finds a token, it allows the request or proceeds. If not, perhaps the authorizer itself will respond to the request with an HTTP error code or redirect code to send the user to a login page. But, otherwise, the authorizer will pass the request to the next piece of middleware. Perhaps this piece of middleware is a router. A router looks at the URL and determines where you want to go next. Do you want to call some method on a class? And that method, it might return JSON data or XML data or an HTML page. The router could look all over the application for something to respond. And if the router doesn't find anything to respond, the router itself might return a 404 Not Found error. Or if it found the right component and that component produces HTML, well now the pipeline starts to reverse itself because the router can return control to the authorizer, and the authorizer can return control to the logger, and when the logger at this point sees that the rest of the pipeline has finished, it might record that fact and log the total amount of time taken to process this particular request. And then it will allow the request to flow out over the server and over the network to the client who is waiting for some result. 

* This is in essence what middleware is all about in ASP.NET. We will need middleware to handle errors. We will need middleware to serve static files from the file system. And we will need middleware to send HTTP requests to the MVC framework, which will ultimately allow us to build this application later that will show restaurant information to the users.

### 2.2 Using IApplicationBuilder
* Inside of Configure method we will invoke extension methods on the IApplicationBuilder interface to add middleware.

* The IISPlatformHandler middleware allows us to use Windows authentication tokens that IIS sends along, and we will leave this middleware here even though we don't plan on using Windows authentication for this application. We are going to need to add all middleware, and most middleware components are stored in separate NuGet packages. 

* Configure method in Startup.cs is used to configure the HTTP request pipeline. We configure the pipeline by programming against the object that implements IApplicationBuilder, and there will be methods available to us to add additional pieces of middleware to the request pipeline. There are two pieces of middleware in a new empty project by default. One is the IISPlatformHandler. That allows us to work with Windows authentication. What IISPlatformHandler will do is look at every incoming request and see if there is any Windows identity information associated with that request. And then it calls the next piece of middleware. 

* The Run method allows us to pass in another method, which we can use to process every single response. Run is not something that you'll see very often. It's something that we call a terminal piece of middleware. Middleware that you register with Run will never have the opportunity to call another piece of middleware. All it does is receive a request, and then it has to produce some sort of response. When you write a method, you get a parameter that is of type HTTP context. Every piece of middleware receives this HTTP context. This is how you can look at things like the Request object and look at headers that are available inside of the request.

* The Microsoft.AspNet.Diagnostics package is ASP.NET middleware for exception handling, exception display pages, and diagnostics information. This one package contains many different pieces of middleware that I can use.app.After installing Microsoft.AspNet.Diagnostics package they are some additional methods that are available extension methods for IApplicationBuilder.

* The order of the middleware is important. If we were to place UseRuntimeInfoPage as the last piece of middleware in the pipeline after app.Run, we would never be able to get to this page because that piece of middleware would never get invoked. App.Run never calls into the next piece of middleware. So we use this before app.Run. And then something else to know about middleware, in general, is that most pieces of middleware will allow you to pass in some options to configure that middleware. Example: we can change the path that UseRuntimeInfoPage middleware is responding to.

* Many pieces of middleware will also include a full options object that you can instantiate and pass in and that will give you the ability to configure all of the possible options for that particular piece of middleware.

* Every ASP.NET application will need some middleware registered if it's going to perform any useful work. A typical workflow for registering middleware is, first, you generally need some sort of package that will give you access to the middleware. And once you have that package installed, there will be additional methods on IApplicationBuilder that you can invoke to put that middleware into the pipeline and optionally configure it.
