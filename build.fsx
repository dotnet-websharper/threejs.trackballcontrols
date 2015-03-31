#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.ThreeJs.TrackballControls", "3.0")
    |> fun bt -> bt.WithFramework(bt.Framework.Net40)

let main =
    bt.WebSharper.Extension("WebSharper.ThreeJs.TrackballControls")
        .SourcesFromProject()
        .Embed(["TrackballControls.js"])
        .References(fun r ->
            [r.NuGet("WebSharper.ThreeJs").Reference()]
        )

//let test =
//    (bt.WebSharper.BundleWebsite("WebSharper.ThreeJs.TrackballControls.Tests")
//    |> FSharpConfig.BaseDir.Custom "Tests")
//        .SourcesFromProject("Tests.fsproj")
//        .References(fun r -> [r.Project main])

bt.Solution [
    main
//    test

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.ThreeJs.TrackballControls"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.org/intellifactory/websharper.threejs.trackballcontrols"
                Description = "WebSharper Extensions for ThreeJs.TrackballControls 20140320"
                Authors = ["IntelliFactory"]
                RequiresLicenseAcceptance = true })
        .Add(main)

]
|> bt.Dispatch
