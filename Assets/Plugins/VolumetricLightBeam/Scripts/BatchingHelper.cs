// Force isDepthBlendEnabled at true when GPU Instancing is enabled, to prevent from breaking the batch if 1 beam has it at 0 and 1 has it at > 0
#define FORCE_ENABLE_DEPTHBLEND_FOR_BATCHING

using UnityEngine;

namespace VLB
{
    public static class BatchingHelper
    {
#if FORCE_ENABLE_DEPTHBLEND_FOR_BATCHING
        public static bool forceEnableDepthBlend { get { return Config.Instance.actualRenderingMode == RenderingMode.GPUInstancing || Config.Instance.actualRenderingMode == RenderingMode.SRPBatcher; } }
#else
        public const bool forceEnableDepthBlend = false;
#endif

        public static bool IsGpuInstancingEnabled(Material material)
        {
            Debug.Assert(material != null);
            return material.enableInstancing;
        }

        public static void SetMaterialProperties(Material material, bool enableGpuInstancing)
        {
            Debug.Assert(material != null);
            material.enableInstancing = enableGpuInstancing;
        }

        public static bool CanBeBatched(VolumetricLightBeam beamA, VolumetricLightBeam beamB, ref string reasons)
        {
            if (Config.Instance.actualRenderingMode != RenderingMode.GPUInstancing && Config.Instance.actualRenderingMode != RenderingMode.SRPBatcher)
            {
                reasons = string.Format("Current Render Pipeline is '{0}'. To enable batching, use 'GPU Instancing'", Config.Instance.renderPipeline);
                if(Config.Instance.renderPipeline != RenderPipeline.BuiltIn)
                    reasons += " or 'SRP Batcher'";
                return false;
            }

            bool ret = true;

            if (!CanBeBatched(beamA, ref reasons))
                ret = false;

            if (!CanBeBatched(beamB, ref reasons))
                ret = false;

            if (Config.Instance.featureEnabledDynamicOcclusion)
            {
                if ((beamA.GetComponent<DynamicOcclusionAbstractBase>() == null) != (beamB.GetComponent<DynamicOcclusionAbstractBase>() == null))
                {
                    AppendErrorMessage(ref reasons, string.Format("{0}/{1}: dynamically occluded and non occluded beams cannot be batched together", beamA.name, beamB.name));
                    ret = false;
                }
            }

            if (Config.Instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off && beamA.colorMode != beamB.colorMode)
            {
                AppendErrorMessage(ref reasons, string.Format("'Color Mode' mismatch: {0} / {1}", beamA.colorMode, beamB.colorMode));
                ret = false;
            }

            if (beamA.blendingMode != beamB.blendingMode)
            {
                AppendErrorMessage(ref reasons, string.Format("'Blending Mode' mismatch: {0} / {1}", beamA.blendingMode, beamB.blendingMode));
                ret = false;
            }

            if (Config.Instance.featureEnabledNoise3D && beamA.isNoiseEnabled != beamB.isNoiseEnabled)
            {
                AppendErrorMessage(ref reasons, string.Format("'3D Noise' enabled mismatch: {0} / {1}", beamA.noiseMode, beamB.noiseMode));
                ret = false;
            }

            if (Config.Instance.featureEnabledDepthBlend && !forceEnableDepthBlend)
            {
#pragma warning disable 0162
                if ((beamA.depthBlendDistance > 0) != (beamB.depthBlendDistance > 0))
                {
                    AppendErrorMessage(ref reasons, string.Format("'Opaque Geometry Blending' mismatch: {0} / {1}", beamA.depthBlendDistance, beamB.depthBlendDistance));
                    ret = false;
                }
#pragma warning restore 0162
            }

            if (Config.Instance.featureEnabledShaderAccuracyHigh && beamA.shaderAccuracy != beamB.shaderAccuracy)
            {
                AppendErrorMessage(ref reasons, string.Format("'Shader Accuracy' mismatch: {0} / {1}", beamA.shaderAccuracy, beamB.shaderAccuracy));
                ret = false;
            }

            return ret;
        }

        public static bool CanBeBatched(VolumetricLightBeam beam, ref string reasons)
        {
            bool ret = true;

            if (Config.Instance.actualRenderingMode == RenderingMode.GPUInstancing)
            {
                if (beam.geomMeshType != MeshType.Shared)
                {
                    AppendErrorMessage(ref reasons, string.Format("{0} is not using shared mesh", beam.name));
                    ret = false;
                }
            }

            if (Config.Instance.featureEnabledDynamicOcclusion && beam.GetComponent<DynamicOcclusionDepthBuffer>() != null)
            {
                AppendErrorMessage(ref reasons, string.Format("{0} is using the DynamicOcclusion DepthBuffer feature", beam.name));
                ret = false;
            }
            return ret;
        }

        static void AppendErrorMessage(ref string message, string toAppend)
        {
            if (message != "") message += "\n";
            message += "- " + toAppend;
        }
    }
}
