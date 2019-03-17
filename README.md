# Aussiepay
Code for custom Middleware for REST API Plugin

Please check the Plugin development requirement in HTTP 418 requirement.docx file

Steps to achieve this

1) I have created a Class library for Middleware
2) Add reference to Class library in the Sample.WebAPI project and Injected the middleware in Startup.cs file in Sample.WebAPi Project in the function below

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add the middleware in the api to catch the request and display status code accordingly
            app.UseMiddleware<RequestVerifier>();

            //app.UseMiddleware<RequestTime>();
            app.UseMvc();
        }
3) Run the test cases in CustomMiddleware Test project. The RequestVerifierTest.cs file contains below test 
	a) WhenMethodNot_GETPOSTDELTE() -- to test when the method is not GET/POST/DELETE
	b) WhenMethodGET() - to test the middleware when the conditions are method
	c) WhenMethodPOST() - to test the middleware when the conditions are method
	d) WhenMethodDELETE() - to test the middleware when the conditions are method		



4) Restore Nuget packages in case of any issue.
