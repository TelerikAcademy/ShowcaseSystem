# Showcase System
Showcase system for students' projects in Telerik Academy

### Build status

[![Build status](https://ci.appveyor.com/api/projects/status/m0g2ef0358847bra?svg=true)](https://ci.appveyor.com/project/NikolayIT/showcasesystem)

### How to run
- Install [NodeJS](http://nodejs.org/)
- Install [Gulp](http://gulpjs.com/) globally with

		npm install -g gulp
		
- Navigate the terminal to the client folder

		\Source\Client\Showcase.Client.App
		
- Install missing NPM packages with 

		npm install
		
- Run the Gulp watch task with

		gulp watch
		
- For minified scripts and styles use

		gulp watch --env production
		
- Or optionally use the [Task Runner Explorer](https://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708) extension to Visual Studio 2013 which is already configured to run the Gulp watch task on every solution build
