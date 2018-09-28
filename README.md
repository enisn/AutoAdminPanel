# AutoAdmin.Mvc
<hr />
<p> Provides an Admin Panel into your project.</p>

<a href="https://www.nuget.org/packages/AutoAdmin.Mvc/"><img src="https://img.shields.io/badge/Nuget-0.9.3-blue.svg" /></a>


## SET-UP

Add nuget to your project.

Go to your **App_Start/RouteConfig.cs** and change your routing to:

```csharp
      routes.MapRoute(
      name: "Default",
      url: "{controller}/{table}/{action}/{id}",

      defaults: new { controller = "Home", action = "Index",table = "Categories", id = UrlParameter.Optional }
      );
```

simply you need to add {table} parameter.
<hr />



Then you must go to your        **Global.asax**        file and add this into      **Application_Start()**      method:

```csharp
Configuration.Init(typeof(Models.NORTHWNDEntities));
```


The ```typeof(Models.NORTHWNDEntities)``` parameter must be your **DbContext** class.

<hr />
Then you can create menu for your admin panel.

```csharp
      @foreach (var item in AutoAdmin.Mvc.Helpers.QueryHelper.GetTableNames())
      {
          <li>
          <a href="@Url.Action("Index","Admin",new { table=item })">Index</a>
          </li>
      }
```

      Your menu and your admin panel is ready to launch !!!!