# Notes about ASP.NET Core RC1

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

### 2.3 Show exception details 
* The "app.UseDeveloperExceptionPage" doesn't care so much about the incoming requests.
It's call the next piece of middleware. It's wait to see if anything later in the pipeline generates an exception.
And if there is an exception, this piece of middleware will give us an error page with some info.

### 2.4 Middleware to match the environment
* ASP.NET is looking for the parameters that we passed to the Configure method.
It's analyzing those and going to the service container to figure out what object to pass.

* IHostingEnvironment give us info about the environment running inside of. 

## 3 Controllers in the MVC framework
### 3.1 MVC(Model View Controller) design pattern
* The MVC design patters is a popular design pattern for the user interface layer of a software application.
In MVC, the controller receives an HTTP request. The controller builds a model in the most of cases. The model is only responsible to hold the needed info that the user wants to see. After that the controller can select a view to render the model. The idea behind the MVC pattern is to keep a separation of concerns. The controller is only responsible for taking a request and building a model. And it's the model that carries the logic and data we need into the view. And then the view is only responsible for transforming that model into HTML.

### 3.2 Routing
* The MVC middleware will make dicision about "How to determine if a given HTTP request should go to a controller for processing or not". The MVC middleware take this decision base on the URL and the configure information we provide. One way to define this configuration infromation is to define the routes for our controller insinde in Startup.cs class when we add the MVC middleware(this is a convention-bas routing). Another approach to routing is attribute-based routing.

### 3.3 Attribute routes
* Attribute-based routing is great for special case routes or actions that need some additional parameters.

### 3.4 Action Results
* It's common to derive a controller from a controller base class provided in "Microsoft.AspNet.Mvc" namespace.
This base class give us access to lots of info about a request. As well a methods help me to build results to send back to client. So I inherit this base class in my controllers. After that we have a bigger API. They're a lot of properties that represent contextual info.

* The advantage of using IActionResult is that it's a formal way to encapsulate the decesion of controller. So the controller decides what to return (string, HTML, JSON). This give us more flexability and makes controllers a little bit more testable.

### 3.5 Additional notes:
* Anytime you use something that is just a list of something, list isn't thread safe, so you have to be a little bit careful using it in a web application. But I'm going to assume we only use InMemoryRestaurantData when developing or when testing, so we're not going to have to deal with multiple users. When we actually deploy the application, we'll use a real SQL Server database. And a database can handle multiple users easily.

## 4. Models in the MVC Framework
### 4.1 Accepting From Input
* If my restaurant has any properties or any attributes that I don't want network data to get into, then I am much better off not using an entity but using an input view model. It's much safer.

### 4.2 Model validation with data anotations
* Html.EditorFor() helper give us the best editor based on the info you have available.

## 5. Using Entity Framework
### 5.1 SQL Server LocalDB
Notes: This is a special edition of SQL Server that is optimized for developers.
When check "Microsoft SQL Server Data tools" from the installer of Visual Studio we install some additional useful tools.

### 5.2 Installing the Entity Framework
* The commands in the "commands" section in project.json file you can use from the .NET command line tool.

### 5.3 Implementing a DbContext
* To use the Entity Framework, we need to create a class that derives from the Entity Framework DBContext base class. Each DBContext class that you write will give you access to a single database.

### 5.4 Entity Framework Migrations
* To achieve this thing, I used the Developer Command Prompt for VS2015.

* First we use "dnvm" (.Net Version Manager), after that we use "dnvm list". This command shows you your runtimes.
Next, we need to select one from this runtimes, we use the command "dnvm use {name of the Runtime}" and that will give us access to the dnx (.NET Execution Environment). If we include the "-p" afther the name of the runtime, then we won't have to execute this command again unless we want to change the runtime that we using.
Using dnx, we can call into commands that we have listed in the project.json file.

* After that we can just type "dnx ef" that will show us info about the EntityFramework.Commands.
We can type "dnx ef migrations" and we will get a help screen again.
Next, we execute the "dnx ef migrations add {the name of migration}". This command will generates the first migration or add new migration if we already have old migrations. And by default the code is generated in "Migrations" folder.

* Next, we can update the dabatase with using the "dnx ef dabatase update".
At the first time, when we don't have database yet, this command will generate the dabatase.
This command applying the last migration.

## 6. Razor Views
### 6.1 _ViewStart
* The Razor view engine in MVC has a convention where it will look for any file with the name _ViewStart.cshtml and execute the code inside of this file before executing the code inside of an individual view. The code inside of a ViewStart file cannot render into the HTML output of a page, but it can be used to remove duplicate code from the code blocks inside of the individual views.

### 6.2 _ViewImports
* In addition to the ViewStart file, there also a ViewImports file that the MVC framework will look for when rendering any view. Like the ViewStart file, I can drop ViewImports.cshtml into a folder, and the ViewImports file can influence all the views in the folder hierarchy. This view is new for this version of MVC, and here's the idea. In previous versions of MVC, we could use an XML configuration file to configure certain aspects of the Razor view engine. Those XML files are gone. I use code instead. The ViewImports file is a place where I can write code and place common using directives to pull in namespaces that my views need.

### 6.3 Tag helpers
* Tag helpers are a new feature of this version of MVC. Tag helpers, just like HTML helpers, will help us render HTML. In many cases, you can replace a HTML helper with a tag helper. Why would we use tag helpers instead of HTML helpers? Tag helpers blend in to the HTML.

* Any parameters that you want passed along with a tag helper, you can use the "asp-route" tag helper. In this tag helper we add the identifier for the data after "asp-route". In other words, if I want to include a parameter in the URL that is named Id, then I can use asp-route-Id. If, instead, I wanted to pass something called title, I could use asp-route-title. And I could have both of those, -Id and -title, if I needed two pieces of information to build the complete URL.

### 6.4 Partial views
* There are two use cases for partial views. One use of partial views is when you have some view code that you want to re-use in the application. Let's say, for example that you have a Layout view, and the Layout view is selected by an Index view that renders in the body of that Layout view. The Index view receives from the controller a model object that is a list of restaurants. And the Index view has to display summary information about each restaurant. And this restaurant summary might require a block of Razor code that also appears in other views that are And this restaurant summary might require a block of Razor code that also appears in other views that are displaying restaurant information. If that's the case, I can take the common code and put it in its own view, let's call it _Summary, and render that partial view from the Index view using an HTML helper named Partial.

* With Partial, I pass the name of a view and optionally a model object for that partial view to work with. So you can also use partial views to take a complex model object and break down the rendering of that object into smaller views. The key point here is that when using Html. Partial to render a partial view, the partial view relies on model data from the parent view. The partial view cannot go out and get a model independently.

* If you do need a different model for a partial view, this is where view components come into play. View components are new in this version of MVC. Imagine I have a layout page that is again selected by the Index view, and the Index view has a model which is a list of restaurants. But a Layout view also needs to display some advertisements about restaurants using information from the database. I don't want the Index view to provide that ad data since the Index view is only concerned with the restaurants themselves. This is where I can build a view component and use Component.Invoke to render a component anywhere on the page. I can use Component.Invoke from an Index view or a Layout view or any other partial view. And the wonderful part about view components is that they are separate objects, which can be instantiated and perform their own data access, build their own model objects, and render their own partial views. So unlike Html.Partial, a view component doesn't rely on the parent view for anything.

### 6.5 View components
* Now a view component is more than just a partial view. A view component is almost a complete MVC abstraction. There is a class with a method that will get invoked. The method needs to build a model and pick which view to render. It's just that that view is going to be a partial view that appears to render part of the page. If you have used previous versions of MVC, you might have used the Html.Action helper to execute a child action. In this version of the MVC framework, child actions are gone, and we use view components instead. It's a much better solution to this problem of I want an independent component that can render a partial view with its own model into my page. To create this view component, we need to create a new folder. So in addition to the controller's folder, we also need to to have ViewComponents. And inside of ViewComponents, we going to add a class because, as I said, a view component is really all the MVC pieces put together. You can almost think of the view component as a controller. We just never route to it.

* Now one word of warning about the View helper method, and this is true both for view components as well as controllers, if your model is a simple string and you pass this string as the first parameter to the View method, the MVC framework sees that as the name of the view that you want to render. It doesn't see it as the model object. If I want a string to be my model object, I have to explicitly specify my view name and then pass in the model object.

### 7.1 Identity Framework Overview
* The Identity framework is another dependency that we need to add to our application in the project.js file. This framework allows us to add features where users can register and log in with a local password. The framework also supports two-factor authentication, third-party identity providers, account lockouts, and other features.

* The UserStore is the class that our code will talk to to create users and validate user passwords. Ultimately, a UserStore will talk to a database, and out of the box the Identity framework supports the Entity Framework and all of the databases that can work with the Entity Framework.

### 7.2 Creating a user
* We need a "UserManager<User>" and a "SignInManager<User>" in AccountController.
Use them to create the user and if the user is created successfully then login the user. Otherwise we show to the anonymos user error message about the creating of user account.

### 7.3 Log in and log out
* If there is a return URL, I also want to make sure that the URL is a local URL. Otherwise, we would have a website with a security vulnerability known as an open redirect.

## 8. Front End Frameworks and Tools
### 8.1 Front End Tools
* If you want to use jQuery in your application, you can install jQuery with npm and skip using Bower and NuGet.

### 8.2 Command line versus Visual Studio
* There're a few different ways that you can work with npm in an ASP.NET project. One approach is to use tooling and the support provided by Visual Studio. This is the approach that I will show you in this module. Another approach is to use Node and npm from the command line. The command line approach gives you flexibility and power beyond what the tooling in Visual Studio provides, but you may or may not need that power. There are other Pluralsight courses that can show you the command line interface.  The Visual Studio approach that we'll be using will behind the scenes use a version of Node and npm that Visual Studio 2015 installs by default with the rest of the web tools. If you are experienced with the command line, I recommend you download and install Node.js from nodejs.org so you have more control over the environment. In this module, we will rely on the tools that Visual Studio has installed. And to see how this works as a first step, let's see if we can download the Bootstrap CSS framework. 

### 8.3 Setting up the npm
* project.json manages our server-side dependencies, frameworks like the MVC framework. We're also need to add a JSON configuration file for npm to manage our front-end dependencies.

### 8.4 Enable Client-side Validation
* The whole idea behind data- attributes is that they're custom for your application, and your application will have to provide some JavaScript to act on those attributes. Fortunately, Microsoft provides some JavaScript that we can use to consume this validation metadata and enforce validation on the client, the script that Microsoft includes built on top of a popular jQuery plugin known as jQuery Validation.

* jQuery has to come first before any plugins.

### 8.5 Using the CDNs and Fallbacks
* A CDN can typically deliver script files faster than your own local servers.
This is a CDN run by Microsoft: http://asp.net/ajax/cdn. On this page we can see the list of all libraries that are hosted on Microsoft CDN.
