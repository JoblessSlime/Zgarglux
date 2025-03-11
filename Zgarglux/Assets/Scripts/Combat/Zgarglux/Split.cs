using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public GameObject splitPrefab; // SplitPrefab is kinda the same but with the SplitAI script (in fact, maybe not)
    public SlimeInfos slimeInfos;

    public void SplitSlime(GameObject actualSlime)
    {
        GameObject split1 = Instantiate(actualSlime, actualSlime.transform.position, Quaternion.identity);
        GameObject split2 = Instantiate(actualSlime, actualSlime.transform.position, Quaternion.identity);

        slimeInfos.splits.Remove(actualSlime);

        slimeInfos.splits.Add(split1);
        slimeInfos.splits.Add(split2);

        split2.GetComponent<SplitAI>().AIisActive = true;
        split2.GetComponent<CharacterController>().enabled = false;
        split2.GetComponent<Rigidbody>().isKinematic = false;
        split2.GetComponent<Rigidbody>().mass = 1;

        split2.transform.GetChild(2).gameObject.SetActive(false);

        slimeInfos.activeSplit = split1;

        Destroy(actualSlime);
    }

    public void MergeSlime(GameObject split1, GameObject split2)
    {
        GameObject merged = Instantiate(splitPrefab, Vector3.Lerp(split1.transform.position, split2.transform.position, 0.5f), Quaternion.identity);
        slimeInfos.splits.Add(merged);
        slimeInfos.splits.Remove(split1);
        slimeInfos.splits.Remove(split2);
        Destroy (split1);
        Destroy(split2);

        merged.GetComponent<SplitAI>().AIisActive = false;
        merged.transform.GetChild(2).gameObject.SetActive(true);
        slimeInfos.activeSplit = merged;
        slimeInfos.healthPoint = 100;
    }

    public void SwitchSplit()
    {

    }
}
