using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Saito
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UI/ClickButton", 14)]
    public class ClickButton : MonoBehaviour
    {
        [Header("�������ۂ̃{�^���摜")] [SerializeField] private Sprite pressedSprite=null;
        [Header("�������ۂɌĂяo�����֐�")] [SerializeField] private UnityEvent onClick=null;

        private Sprite _defaultSprite;

        private void Awake()
        {
            _defaultSprite = this.gameObject.GetComponent<Image>().sprite;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            //�N���b�N��������s�����
            this.gameObject.AddComponent<ObservablePointerClickTrigger>()
                .OnPointerClickAsObservable().Subscribe(_ =>
                {
                    TestOnClick();
                    onClick.Invoke();
                },
                ex => Debug.LogError("������܂���")
            ).AddTo(this.gameObject);
            
            this.gameObject.AddComponent<ObservablePointerDownTrigger>()
                .OnPointerDownAsObservable().Subscribe(_ =>
                    {
                        OnButtonDown();
                    },
                    ex => Debug.LogError("������܂���")
                ).AddTo(this.gameObject);

            this.gameObject.AddComponent<ObservablePointerUpTrigger>()
                .OnPointerUpAsObservable().Subscribe(_ =>
                    {
                       OnButtonUp();
                    },
                    ex => Debug.LogError("������܂���")
                ).AddTo(this.gameObject);
            
            this.gameObject.AddComponent<ObservablePointerExitTrigger>()
                .OnPointerExitAsObservable().Subscribe(_ =>
                    {
                        OnButtonExit();
                    },
                    ex => Debug.LogError("������܂���")
                ).AddTo(this.gameObject);
            
            this.gameObject.AddComponent<ObservablePointerEnterTrigger>()
                .OnPointerEnterAsObservable().Subscribe(_ =>
                    {
                        OnButtonEnter();
                    },
                    ex => Debug.LogError("������܂���")
                ).AddTo(this.gameObject);

            this.gameObject.AddComponent<ObservableSelectTrigger>()
                .OnSelectAsObservable().Subscribe(_ =>
                    {
                        OnButtonSelect();
                    },
                    ex => Debug.LogError("������܂���")
                ).AddTo(this.gameObject);

            /*
            var mouseDownStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
            var mouseUpStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0));
    
            //�������̔���
            //�}�E�X�N���b�N���ꂽ��3�b���OnNext�𗬂�
            mouseDownStream
                //3�b���ƂɃ��b�Z�[�W�𔭐�������
                .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(3)))
                //�}�E�X�̃N���b�N�𗣂�����A���b�Z�[�W���M���I������
                .TakeUntil(mouseUpStream)
                .RepeatUntilDestroy(this.gameObject)
                .Subscribe(_ => Debug.Log("������"))
                .AddTo(this);
            
            //��ɔ��M����C�x���g
            this.UpdateAsObservable().
                Where(_ => Input.GetKeyDown(KeyCode.Space)) //Space�L�[��������Ă���
                .TakeUntilDestroy(this)
                .ThrottleFirst(TimeSpan.FromSeconds(2)) //2�b�̊Ԋu�Ŕ��M����
                .Subscribe(_ =>
                {
                    Debug.Log("Space Key Down!");
                    _text.text="Space!";
                }); //���s�����C�x���g�n���h���[
                */
        }
        
        
        #region Button�Ŏ��s���������ʏ���

        private void OnButtonExit()
        {
            // Exit���̋��ʏ���
            Debug.Log("Button  Exit");
        }

        private void OnButtonEnter()
        {
            // Enter���̋��ʏ���
            Debug.Log("Button Enter");
        }

        private void OnButtonDown()
        {
            // Down���̋��ʏ���
            this.gameObject.GetComponent<Image>().sprite=pressedSprite;
            Debug.Log("Button Push");
        }

        private void OnButtonUp()
        {
            // Up���̋��ʏ���
            this.gameObject.GetComponent<Image>().sprite=_defaultSprite;
            Debug.Log("Button Release");
        }

        private void OnButtonClick()
        {
            // Click���̋��ʏ���
            Debug.Log("Button Click");
        }

        private void OnButtonSelect()
        {
            // Select���̋��ʏ���
            Debug.Log("This gameObject Selected!");
        }
        
        /// <summary>
        /// �N���b�N���ɌĂяo�����֐�(�o�^)
        /// </summary>
        public void TestOnClick()
        {
            Debug.Log("Event OnClick!");
        }
        
        #endregion
    }
}