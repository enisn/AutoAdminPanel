 Add nuget to your project.

      Go to Your App_Start/RouteConfig.cs and change your routing to:
      ------------------------
      routes.MapRoute(
      name: "Default",
      url: "{controller}/{table}/{action}/{id}",

      defaults: new { controller = "Home", action = "Index",table = "Categories", id = UrlParameter.Optional }
      );
      ------------------------
      simply you need to add {table} parameter.

      You must go to your        Global.asax        file and add this into      Application_Start()      method:

      Configuration.Init(typeof(Models.NORTHWNDEntities));



      The             typeof(Models.NORTHWNDEntities)          parameter must be your DbContext class.


      Then you can create menu for your admin panel.


      @foreach (var item in AutoAdmin.Mvc.Helpers.QueryHelper.GetTableNames())
      {
          <li>
          <a href="@Url.Action("Index","Admin",new { table=item })">Index</a>
          </li>
      }


      Your menu and your admin panel is ready to launch !!!!