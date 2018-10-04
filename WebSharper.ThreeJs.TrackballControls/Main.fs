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
