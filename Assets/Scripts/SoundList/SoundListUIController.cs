using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoundListUIController : MonoBehaviour
{
    // SoundListControllerでも使う変数
    [HideInInspector]
    public int index = 0;

    #region
    [SerializeField]
    private AudioSource SEaudioSource;
    [SerializeField]
    private AudioClip SEaudioClip;

    [SerializeField]
    private SoundListController soundListController;
    [SerializeField]
    private RectTransform record;
    [SerializeField]
    private RectTransform recordPlayerNeedle;
    [SerializeField]
    private RectTransform musicList;
    [SerializeField]
    private RectTransform selection;
    [SerializeField]
    private RectTransform maskRectTransform;
    [SerializeField]
    private Image maskImage;
    [SerializeField]
    private float[] selectionLocation;
    #endregion

    #region
    private Tween recordTween;
    private Tween recordPlayerNeedleTween;
    private float verticalKey;
    private bool push = false;
    #endregion

    private void Start()
    {
        recordTween = record.transform.DOLocalRotate(new Vector3(0, 0, -360f), 6f, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart)
                    .SetLink(record.gameObject);
        recordTween.Pause();
        
        recordPlayerNeedleTween = recordPlayerNeedle.transform.DOLocalRotate(new Vector3(0, 0, -2f), 3f, RotateMode.Fast)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetLink(recordPlayerNeedle.gameObject);
        recordPlayerNeedleTween.Pause();

        maskImage.enabled = false;
    }

    private void Update()
    {
        verticalKey = Input.GetAxisRaw("Vertical");
        if (!push && verticalKey < 0)
        {
            push = true;
            ++index;
            if (index > 13)
            {
                index = 0;
            }
            soundListController.firstPushY = false;
            SEaudioSource.PlayOneShot(SEaudioClip);
            selection.anchoredPosition = new Vector2(-690.0f, selectionLocation[index]);
            maskRectTransform.anchoredPosition = new Vector2(-690.0f, selectionLocation[index]);
        }
        if (!push && verticalKey > 0)
        {
            push = true;
            --index;
            if (index < 0)
            {
                index = 13;
            }
            soundListController.firstPushY = false;
            SEaudioSource.PlayOneShot(SEaudioClip);
            selection.anchoredPosition = new Vector2(-690.0f, selectionLocation[index]);
            maskRectTransform.anchoredPosition = new Vector2(-690.0f, selectionLocation[index]);
        }

        if (soundListController.soundAudioSource.isPlaying)
        {
            recordTween.Play();
            recordPlayerNeedleTween.Play();
            maskImage.enabled = true;
        }
        else if (!soundListController.soundAudioSource.isPlaying)
        {
            recordTween.Pause();
            recordPlayerNeedleTween.Pause();
            maskImage.enabled = false;
        }

        if (index == 12)
        {
            musicList.anchoredPosition = new Vector2(0.0f, 89.0f);
        }
        else if (index == 13)
        {
            musicList.anchoredPosition = new Vector2(0.0f, 181.0f);
        }
        else
        {
            musicList.anchoredPosition = new Vector2(0.0f, 0.0f);
        }

        // 1フレームのうち vertical key が押されていない時があったら
        if (verticalKey == 0)
        {
            // vertical key を押せるようにする
            push = false;
        }
    }
}
