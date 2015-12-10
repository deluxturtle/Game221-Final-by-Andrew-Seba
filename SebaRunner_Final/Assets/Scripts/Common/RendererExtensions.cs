using UnityEngine;
using System.Collections;

public static class RendererExtensions {

    //Found on http://wiki.unity3d.com/index.php?title=IsVisibleFrom
    //By: Michael Garforth
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        //Creates planes from the camera frustrum or field of view
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        //Check to see if renderer bounds or the object we are checking
        //are inside the planes
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
