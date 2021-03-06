﻿using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace LargerSolar
{
    [StaticConstructorOnStartup]
    public class CompPowerPlantLargeSolar : CompPowerPlant
    {
        private float FullSunPower = 2700f;

        private const float NightPower = 0f;

        private static readonly Vector2 BarSize = new Vector2(2.3f, 0.14f);

        private static readonly Material PowerPlantSolarBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f));

        private static readonly Material PowerPlantSolarBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f));

        protected override float DesiredPowerOutput
        {
            get
            {
                return Mathf.Lerp(0f, FullSunPower, SkyManager.CurSkyGlow) * this.RoofedPowerOutputFactor;
            }
        }

        private float RoofedPowerOutputFactor
        {
            get
            {
                int num = 0;
                int num2 = 0;
                foreach (IntVec3 current in this.parent.OccupiedRect())
                {
                    num++;
                    if (Find.RoofGrid.Roofed(current))
                    {
                        num2++;
                    }
                }
                return (float)(num - num2) / (float)num;
            }
        }

       public override void PostDraw()
		{
			base.PostDraw();
			GenDraw.FillableBarRequest r = default(GenDraw.FillableBarRequest);
			r.center = this.parent.DrawPos + Vector3.up * 0.1f;
            r.size = CompPowerPlantLargeSolar.BarSize;
			r.fillPercent = base.PowerOutput / 2700f;
            r.filledMat = CompPowerPlantLargeSolar.PowerPlantSolarBarFilledMat;
            r.unfilledMat = CompPowerPlantLargeSolar.PowerPlantSolarBarUnfilledMat;
			r.margin = 0.15f;
			Rot4 rotation = this.parent.Rotation;
			rotation.Rotate(RotationDirection.Clockwise);
			r.rotation = rotation;
			GenDraw.DrawFillableBar(r);
		}
    }
}