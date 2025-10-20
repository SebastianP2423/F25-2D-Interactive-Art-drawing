// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;
using System.Collections.Generic;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        Vector2 eyePosition;   // where the eyeball is
        float speed = 3f;      // how fast it moves
        int corneaR = 40;      // eye size
        int irisR = 20;
        int pupilR = 10;

        // trail storage for eyeball movement
        List<Vector2> trail = new List<Vector2>();
        int maxTrailLength = 50;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Craaazy Eyeball");
            Window.SetSize(400, 400);
            eyePosition = new Vector2(200, 200); // starts in the middle of the grid
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            // Eyeball moves toward cursor, making it seem like its following it
            Vector2 mousePosition = Input.GetMousePosition();
            Vector2 toMouse = mousePosition - eyePosition;

            if (toMouse.Length() > 1f)
            {
                Vector2 direction = Vector2.Normalize(toMouse);
                eyePosition += direction * speed;
            }

            // Saves position to trail
            trail.Add(eyePosition);
            if (trail.Count > maxTrailLength)
                trail.RemoveAt(0);

            // Draws Trail but it kept giving me errors and breaking the code when i added any colour
            for (int i = 0; i < trail.Count; i++)
            {
                float alpha = (float)i / trail.Count; // fade factor, i searched up how to do this 
                Draw.LineSize = 0;
                Draw.FillColor = new Color(100, 100, 100, (int)(alpha * 120));
                Draw.Circle(trail[i], corneaR * 0.6f);
            }

            // Function draws the eyeball
            DrawEyeball(eyePosition, corneaR, irisR, pupilR, mousePosition);
        }

        void DrawEyeball(Vector2 position, int corneaR, int irisR, int pupilR, Vector2 mousePosition)
        {
            Vector2 toMouse = mousePosition - position;
            Vector2 direction = toMouse.Length() > 0 ? Vector2.Normalize(toMouse) : Vector2.Zero;
            Vector2 irisPosition = position + direction * (corneaR - irisR);

            // dimensions and color for cornea
            Draw.LineSize = 2;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.White;
            Draw.Circle(position, corneaR);

            // then the same for the iris
            Draw.FillColor = Color.Green;
            Draw.Circle(irisPosition, irisR);

            // now the pupil
            Draw.FillColor = Color.Black;
            Draw.Circle(irisPosition, pupilR);
        }
    }
}

