using Microsoft.Xna.Framework;
using System.Collections;

namespace Sprint0Game
{
    public static class CollisionDetector
    {
        public static void DetectCollisions(ArrayList dynamicObjsLists, ArrayList staticObjsLists)
        {
            ArrayList StaticObjects = new ArrayList();
            ArrayList StaticObjectsCollideList = new ArrayList();
            ArrayList DynamicObjects = new ArrayList();
            ArrayList DynamicObjectsCollideList = new ArrayList();

            foreach (ArrayList list in dynamicObjsLists)
            {
                foreach (IObject obj in list)
                {
                    DynamicObjects.Add(obj);
                    DynamicObjectsCollideList.Add(false);
                }
            }

            foreach (ArrayList list in staticObjsLists)
            {
                foreach (IObject obj in list)
                {
                    StaticObjects.Add(obj);
                    StaticObjectsCollideList.Add(false);
                }
            }

            //check dynamic vs dynamic
            for (int i = 0; i < DynamicObjects.Count; i++)
            {
                for (int n = i+1; n < DynamicObjects.Count; n++)
                {
                    IObject obj1 = (IObject)DynamicObjects[i];
                    IObject obj2 = (IObject)DynamicObjects[n];
                    if(CheckCollisions(obj1, obj2)) 
                    {
                        DynamicObjectsCollideList[i] = true;
                        DynamicObjectsCollideList[n] = true;
                    }
                }
            }

            //check dynamic vs static
            for (int i = 0; i < DynamicObjects.Count; i++)
            {
                for (int n = 0; n < StaticObjects.Count; n++)
                {
                    IObject obj1 = (IObject)DynamicObjects[i];
                    IObject obj2 = (IObject)StaticObjects[n];
                    if (CheckCollisions(obj1, obj2))
                    {
                        DynamicObjectsCollideList[i] = true;
                        StaticObjectsCollideList[n] = true;
                    }
                }
            }

            //all uncollided dynamics
            for (int i = 0; i < DynamicObjects.Count; i++)
            {
                if (!(bool)DynamicObjectsCollideList[i])
                {
                    IObject obj = (IObject)DynamicObjects[i];
                    if (obj.Height * obj.Width != 0 && obj.HasBeenReached)
                        obj.RespondToNoCollision();
                   
                }
            }
        }

        private static bool CheckCollisions(IObject obj1, IObject obj2)
        {
            Rectangle obj1Rect = new Rectangle((int)obj1.CurrentPosition.X, 
                (int)obj1.CurrentPosition.Y - obj1.Height, obj1.Width, obj1.Height);
            Rectangle obj2Rect = new Rectangle((int)obj2.CurrentPosition.X, 
                (int)obj2.CurrentPosition.Y - obj2.Height, obj2.Width, obj2.Height);
            bool collided = false;

            Side obj1Side, obj2Side;
            Rectangle intersectRect = Rectangle.Intersect(obj1Rect, obj2Rect);
            if (intersectRect.Height * intersectRect.Width > 0)
            {
                GetCollisionSides(obj1Rect, obj2Rect, intersectRect, out obj1Side, out obj2Side);
                obj1.RespondToCollision(obj1Side, obj2, intersectRect);
                obj2.RespondToCollision(obj2Side, obj1, intersectRect);
                collided = true;
            }
            return collided;
        }

       private static void GetCollisionSides(Rectangle obj1Rect, Rectangle obj2Rect, Rectangle intersectRect, out Side obj1Side, out Side obj2Side) {

            //determine if mostly side or top/bottom collision
            if (intersectRect.Height > intersectRect.Width)
            {
                //obj1 is on the right
                if (obj1Rect.Center.X > obj2Rect.Center.X)
                {
                    //obj1side
                    if (intersectRect.Center.Y < obj1Rect.Center.Y)
                    {
                        obj1Side = Side.TopOfLeft;
                    }
                    else
                    {
                        obj1Side = Side.BottomOfLeft;
                    }
                    //obj2side
                    if (intersectRect.Center.Y < obj2Rect.Center.Y)
                    {
                        obj2Side = Side.TopOfRight;
                    }
                    else
                    {
                        obj2Side = Side.BottomOfRight;
                    }
                }
                else
                {
                    //obj1side
                    if (intersectRect.Center.Y < obj1Rect.Center.Y)
                    {
                        obj1Side = Side.TopOfRight;
                    }
                    else
                    {
                        obj1Side = Side.BottomOfRight;
                    }
                    //obj2side
                    if (intersectRect.Center.Y < obj2Rect.Center.Y)
                    {
                        obj2Side = Side.TopOfLeft;
                    }
                    else
                    {
                        obj2Side = Side.BottomOfLeft;
                    }
                }
            }
            //top to bottom collision
            else
            {
                if (obj1Rect.Center.Y > obj2Rect.Center.Y)
                {
                    //obj1side
                    if (intersectRect.Center.X < obj1Rect.Center.X)
                    {
                        obj1Side = Side.LeftOfTop;
                    }
                    else
                    {
                        obj1Side = Side.RightOfTop;
                    }
                    //obj2side
                    if (intersectRect.Center.X < obj2Rect.Center.X)
                    {
                        obj2Side = Side.LeftOfBottom;
                    }
                    else
                    {
                        obj2Side = Side.RightOfBottom;
                    }
                }
                else
                {
                    //obj1side
                    if (intersectRect.Center.X < obj1Rect.Center.X)
                    {
                        obj1Side = Side.LeftOfBottom;
                    }
                    else
                    {
                        obj1Side = Side.RightOfBottom;
                    }
                    //obj2side
                    if (intersectRect.Center.X < obj2Rect.Center.X)
                    {
                        obj2Side = Side.LeftOfTop;
                    }
                    else
                    {
                        obj2Side = Side.RightOfTop;
                    }
                }
                
            }
        }
    }
}
