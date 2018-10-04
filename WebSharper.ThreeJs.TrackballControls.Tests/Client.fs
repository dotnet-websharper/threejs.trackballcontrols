// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
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
