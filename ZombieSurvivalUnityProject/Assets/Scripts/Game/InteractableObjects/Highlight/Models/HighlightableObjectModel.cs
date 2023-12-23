using System;
using UnityEngine;

namespace Game.InteractableObjects.Highlight.Models
{
    public class HighlightableObjectModel
    {
        public Func<Renderer> OnGetRenderer { get; set; }

        public Renderer GetRenderer()
        {
            return OnGetRenderer?.Invoke();
        }
    }
}
