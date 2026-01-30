using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Manages body parts and their organization into sections.
/// </summary>
public class BodyComponent
{
    public List<BodyPart> BodyParts { get; private set; }

    public BodyComponent(List<BodyPart> bodyParts)
    {
        BodyParts = bodyParts;

        // Validate total body size
        if (Mathf.Abs(bodyParts.Sum(b => b.bodyPartSize) - 300f) > 0.01f)
        {
            throw new ArgumentException($"Total body parts size must be 300. Current: {bodyParts.Sum(b => b.bodyPartSize)}");
        }
    }

    /// <summary>
    /// Gets all body parts in the specified section.
    /// </summary>
    public List<BodyPart> GetBodyParts(BodySection section)
    {
        return BodyParts.Where(b => b.bodySection == section).ToList();
    }

    /// <summary>
    /// Gets a random body part from the specified section.
    /// </summary>
    public BodyPart GetRandomBodyPart(BodySection section)
    {
        var eligibleParts = GetBodyParts(section);
        if (eligibleParts.Count == 0) return null;

        return eligibleParts[UnityEngine.Random.Range(0, eligibleParts.Count)];
    }

    /// <summary>
    /// Gets body parts from the lower body section.
    /// </summary>
    public List<BodyPart> GetLowerBody() => GetBodyParts(BodySection.LowerBody);

    /// <summary>
    /// Gets body parts from the middle body section.
    /// </summary>
    public List<BodyPart> GetMiddleBody() => GetBodyParts(BodySection.MiddleBody);

    /// <summary>
    /// Gets body parts from the upper body section.
    /// </summary>
    public List<BodyPart> GetUpperBody() => GetBodyParts(BodySection.UpperBody);
}
