namespace TrackballControls

open WebSharper.InterfaceGenerator

module Definition =
    open WebSharper.ThreeJs
    open WebSharper.JavaScript.Dom
    open WebSharper.InterfaceGenerator.Type

    let O = T<unit>

    let TrackballControlsResource =
        Resource "TrackballControls" "TrackballControls.js"
        |> RequiresExternal [T<WebSharper.ThreeJs.Resources.Js>]

    let TrackballControls =
        let ScreenData =
            Class "ScreenData"
            |+> Instance [
                "left"   =? T<int>
                "top"    =? T<int>
                "width"  =? T<int>
                "height" =? T<int>
            ]

        Class "THREE.TrackballControls"
        |=> Inherits T<THREE.EventDispatcher>
        |+> Static [
            Constructor (T<THREE.Object3D>?``object`` * !? T<Node>?domElement)
        ]
        |+> Instance [
            "object"               =? T<THREE.Object3D>
            "domElement"           =? T<Node>
            "enabled"              =@ T<bool>
            "screen"               =? ScreenData
            "rotateSpeed"          =@ T<float>
            "zoomSpeed"            =@ T<float>
            "panSpeed"             =@ T<float>
            "noRotate"             =@ T<bool>
            "noZoom"               =@ T<bool>
            "noPan"                =@ T<bool>
            "noRoll"               =@ T<bool>
            "staticMoving"         =@ T<bool>
            "dynamicDampingFactor" =@ T<float>
            "minDistance"          =@ T<float>
            "maxDistance"          =@ T<float>
            "keys"                 =@ Tuple [T<int>; T<int>; T<int>]

            "handleResize"             => O ^-> O
            "getMouseOnScreen"         => T<int>?pageX * T<int>?pageY * T<THREE.Vector2>?vector ^-> T<THREE.Vector2>
            "getMouseProjectionOnBall" => T<int>?pageX * T<int>?pageY * T<THREE.Vector3>?projection ^-> T<THREE.Vector3>
            "rotateCamera"             => O ^-> O
            "zoomCamera"               => O ^-> O
            "panCamera"                => O ^-> O
            "checkDistances"           => O ^-> O
            "update"                   => O ^-> O
            "reset"                    => O ^-> O
        ]
        |=> Nested [
            ScreenData
        ]
        |> Requires [TrackballControlsResource]

    let Assembly =
        Assembly [
            Namespace "WebSharper.ThreeJs.THREE" [
                 TrackballControls
            ]
            Namespace "WebSharper.ThreeJs.Resources" [
                 TrackballControlsResource
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
