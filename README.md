# TrackballControls

WebSharper binding for TrackballControls, which enables controlling a three.js camera with mouse or touchscreen.

## Example
```fsharp
let camera = new THREE.PerspectiveCamera(45., 16./9.)

let controls = new THREE.TrackballControls(camera)

//---
//Render loop
[<Inline "requestAnimationFrame($0)">]
let render (frame : unit -> unit) = X<unit>

let rec frame () =
	renderer.render(scene, camera)
	controls.Update()

	render frame
//---
```