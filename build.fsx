#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    (BuildTool().PackageId("WebSharper.ThreeJs.TrackballControls", "2.5")
    |> fun bt -> bt.WithFramework(bt.Framework.Net40))
        .References (fun r ->
            [r.NuGet("WebSharper.ThreeJs").Reference()]
        )

let main =
    (bt.WebSharper.Extension("IntelliFactory.WebSharper.ThreeJs.TrackballControls")
    |> FSharpConfig.BaseDir.Custom "TrackballControls")
        .SourcesFromProject("TrackballControls.fsproj")
        .Embed(["TrackballControls.js"])

//let test =
//    (bt.WebSharper.BundleWebsite("IntelliFactory.WebSharper.ThreeJs.TrackballControls.Tests")
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
