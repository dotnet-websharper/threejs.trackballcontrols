namespace Tests

open WebSharper

[<JavaScript>]
module Client =
    open WebSharper.ThreeJs
    open WebSharper.JQuery
    open WebSharper.JavaScript

    [<Inline "requestAnimationFrame($0)">]
    let render (frame : unit -> unit) = X<unit>

    [<SPAEntryPoint>]
    let Main =
        let renderer = new THREE.WebGLRenderer(
                           WebGLRendererConfiguration(
                               Antialias = true
                           )
                       )
        
        renderer.SetSize(1280, 720)
        renderer.SetClearColor(0xffffff)

        JQuery.Of("body").Append(renderer.DomElement) |> ignore
        
        let scene    = new THREE.Scene()
        let cube     = new THREE.Mesh(
                           new THREE.BoxGeometry(1., 1., 1.),
                           new THREE.MeshNormalMaterial()
                       )

        scene.Add(cube)

        let camera   = new THREE.PerspectiveCamera(45., 16./9.)

        camera.Position.Z <- 4.

        scene.Add(camera)

        let controls = new THREE.TrackballControls(camera)

        //---
        let rec frame () =
            renderer.Render(scene, camera)
            controls.Update()

            render frame
        //---

        render frame
