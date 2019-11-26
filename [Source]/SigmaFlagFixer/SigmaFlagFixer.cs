using System.Linq;
using UnityEngine;
using Upgradeables;


namespace SigmaFlagFixerPlugin
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    internal class SigmaFlagFixer : MonoBehaviour
    {
        void Start()
        {
            GameEvents.OnKSCFacilityUpgraded.Add(FixFlags);
            StartCoroutine(CallbackUtil.DelayedCallback(3, FixFlags));
        }

        void OnDestroy()
        {
            GameEvents.OnKSCFacilityUpgraded.Remove(FixFlags);
        }

        void FixFlags(UpgradeableFacility data0, int data1)
        {
            FixFlags();
        }

        void FixFlags()
        {
            PQSCity KSC = FlightGlobals.GetHomeBody()?.pqsController?.GetComponentsInChildren<PQSCity>(true)?.FirstOrDefault(p => p?.name == "KSC");
            SkinnedMeshRenderer[] flags = KSC?.GetComponentsInChildren<SkinnedMeshRenderer>(true)?.Where(smr => smr?.name == "Flag")?.ToArray();
            for (int i = 0; i < flags?.Length; i++)
            {
                flags[i].rootBone = flags[i]?.rootBone?.parent?.gameObject?.GetChild("bn_upper_flag_a01")?.transform;
            }
        }
    }
}
