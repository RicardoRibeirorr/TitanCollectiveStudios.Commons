using System;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors
{
    public enum DefaultColor
    {
        Green,
        Red,
        Blue,
        Yellow,
        Orange,
        Purple,

        QuestSystemColor,
        CheatSystemColor,
        WaypointSystemColor,
    }


    /// <summary>
    /// Specifies a custom color for a component in the Unity Inspector.
    /// </summary>
    /// <remarks>
    /// Supports RGB values, predefined <see cref="DefaultColor"/> values,
    /// and HTML color strings.
    /// </remarks>
    /// <example>
    /// <code>
    /// [ColoredComponent(DefaultColor.Green)]
    /// public class PlayerController : MonoBehaviour
    /// {
    /// }
    ///
    /// [ColoredComponent(0.2f, 0.8f, 0.2f)]
    /// public class EnemyController : MonoBehaviour
    /// {
    /// }
    ///
    /// [ColoredComponent("#4CAF50")]
    /// public class GameManager : MonoBehaviour
    /// {
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class)]
    public class ColoredComponentAttribute : Attribute
    {
        /// <summary>
        /// Gets the color assigned to the decorated component.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Initializes the attribute using RGB color values.
        /// </summary>
        /// <param name="r">The red channel, in the range 0-1.</param>
        /// <param name="g">The green channel, in the range 0-1.</param>
        /// <param name="b">The blue channel, in the range 0-1.</param>
        public ColoredComponentAttribute(float r, float g, float b)
        {
            Color = new Color(r, g, b);
        }

        /// <summary>
        /// Initializes the attribute using a predefined color.
        /// </summary>
        /// <param name="color">
        /// One of the predefined <see cref="DefaultColor"/> values.
        /// </param>
        public ColoredComponentAttribute(DefaultColor color)
        {
            Color = color switch
            {
                DefaultColor.Green => Color.green,
                DefaultColor.Red => Color.red,
                DefaultColor.Blue => Color.blue,
                DefaultColor.Yellow => Color.yellow,
                DefaultColor.Orange => new Color(1f, 0.5f, 0f),
                DefaultColor.Purple => new Color(0.6f, 0.3f, 1f),


                DefaultColor.QuestSystemColor => new Color(0.35f, 1.00f, 0.20f),  // neon green (quest)
                DefaultColor.CheatSystemColor => new Color(1.00f, 0.20f, 0.85f), // neon magenta (cheat)
                DefaultColor.WaypointSystemColor => new Color(1.00f, 0.85f, 0.10f), // neon yellow (waypoint)
                _ => Color.white
            };
        }

        /// <summary>
        /// Initializes the attribute using an HTML color string.
        /// </summary>
        /// <param name="htmlColor">
        /// An HTML color string, such as <c>"#00FF00"</c>, <c>"#FF0000FF"</c>,
        /// or a supported named color.
        /// </param>
        /// <remarks>
        /// If the supplied string cannot be parsed, the resulting color defaults
        /// to <see cref="UnityEngine.Color.clear"/>.
        /// </remarks>
        public ColoredComponentAttribute(string htmlColor)
        {
            Color tempColor;
            ColorUtility.TryParseHtmlString(htmlColor, out tempColor);
            Color = tempColor;
        }
    }
}