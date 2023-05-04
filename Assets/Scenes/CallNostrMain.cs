using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NOSTR
{
    public int Call_NO { get; set; }
    public int Call_S { get; set; }
    public int Call_T { get; set; }
    public int Call_R { get; set; }

    public NOSTR()
    {
        Call_NO = new int();
        Call_S = new int();
        Call_T = new int();
        Call_R = new int();
	}
}


public class CallNostrMain : MonoBehaviour
{
    [SerializeField]
    Button BTN_START;
    [SerializeField]
    Button BTN_POST;
    [SerializeField]
    Button[] BTN_NO;
    [SerializeField]
    Button[] BTN_S;
    [SerializeField]
    Button[] BTN_T;
    [SerializeField]
    Button[] BTN_R;
    [SerializeField]
    Text MSG_NOSTR;
    [SerializeField]
    Text MSG_CLICK_BUTTON;
    [SerializeField]
    Text MSG_GAMEOVER;
    [SerializeField]
    Text MSG_POST_NOSTR;

    enum MODE
    {
        CALL = 0,
        BUTTON,
        NOSTR,
        OVER,
    }
    enum CALLED
    {
        NO = 0,
        S,
        T,
        R,
        END
	}

    NOSTR ans_nostr = new NOSTR();
    NOSTR now_nostr = new NOSTR();
    List<NOSTR> called_nostr = new List<NOSTR>();

    const int CNT_WAIT_MAX_DEFAULT = 20;
    string msg_nostter = "";
    int cnt_wait_max = CNT_WAIT_MAX_DEFAULT;
    int cnt_wait = 0;
    CALLED cld = CALLED.NO;
    CALLED sw_press = CALLED.END;
    int cnt_now = 0;
    int cnt_score = 0;
    int count = 0;
    bool sw_button = false;
    bool sw_over = false;
    MODE mode = MODE.OVER;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        BTN_POST.gameObject.SetActive(false);
        MSG_CLICK_BUTTON.enabled = true;
        MSG_GAMEOVER.enabled = true;
        MSG_POST_NOSTR.enabled = false;
        BTN_NO[0].enabled = false;
        BTN_NO[1].enabled = false;
        BTN_S[0].enabled = false;
        BTN_S[1].enabled = false;
        BTN_T[0].enabled = false;
        BTN_T[1].enabled = false;
        BTN_R[0].enabled = false;
        BTN_R[1].enabled = false;
        cnt_now = 0;
        cld = CALLED.NO;
        sw_over = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case MODE.OVER:
                {
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        if (msg_nostter != "")
                        {
                            Application.OpenURL("https://nostter.vercel.app/post?content=" + msg_nostter);
                        }
                    }
                    if ((count >> 4) % 2 == 0)
                    {
                        MSG_CLICK_BUTTON.enabled = true;
                    }
                    else
                    {
                        MSG_CLICK_BUTTON.enabled = false;
                    }
                }
                break;
            case MODE.CALL:
                if (count == 0)
                {
                    NOSTR tmp = new NOSTR();
                    tmp.Call_NO = Random.Range(1, 3) - 1;
                    tmp.Call_S = Random.Range(1, 3) - 1;
                    tmp.Call_T = Random.Range(1, 3) - 1;
                    tmp.Call_R = Random.Range(1, 3) - 1;
                    called_nostr.Add(tmp);
                    now_nostr.Call_NO = 0;
                    now_nostr.Call_S = 0;
                    now_nostr.Call_T = 0;
                    now_nostr.Call_R = 0;
                    BTN_NO[0].enabled = false;
                    BTN_NO[1].enabled = false;
                    BTN_S[0].enabled = false;
                    BTN_S[1].enabled = false;
                    BTN_T[0].enabled = false;
                    BTN_T[1].enabled = false;
                    BTN_R[0].enabled = false;
                    BTN_R[1].enabled = false;
                    BTN_NO[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_NO[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_S[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_S[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_T[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_T[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_R[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_R[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                    if (called_nostr.Count % 10 == 9)
                    {
                        if (cnt_wait_max > 3)
                        {
                            cnt_wait_max--;
                        }
                    }
                    cnt_wait = 0;
                    cnt_now = 0;
                    sw_button = false;
                    cld = CALLED.NO;
                }
                if (cnt_wait == 0)
                {
                    switch (cld)
                    {
                        case CALLED.NO:
                            BTN_NO[called_nostr[cnt_now].Call_NO].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                            break;
                        case CALLED.S:
                            BTN_S[called_nostr[cnt_now].Call_S].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                            break;
                        case CALLED.T:
                            BTN_T[called_nostr[cnt_now].Call_T].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                            break;
                        case CALLED.R:
                            BTN_R[called_nostr[cnt_now].Call_R].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                            break;
                    }
                    cld++;
                }
                if (cnt_wait >= cnt_wait_max)
                {
                    cnt_wait = -1;
                    if (cld == CALLED.END)
                    {
                        cnt_now++;
                        if (called_nostr.Count <= cnt_now)
                        {
                            BTN_NO[0].enabled = true;
                            BTN_NO[1].enabled = true;
                            cnt_now = 0;
                            count = -1;
                            cld = CALLED.NO;
                            mode = MODE.BUTTON;
                        }
                        else
                        {
                            cld = CALLED.NO;
                            cnt_wait = -1;
                        }
                    }
                }
                cnt_wait++;
                break;
            case MODE.BUTTON:
                {

                    if (sw_button == true)
                    {
                        sw_button = false;
                        Debug.Log("cnt_now=" + cnt_now + "/sw_press=" + sw_press + "/ans_nostr=[" + ans_nostr.Call_NO + "][" + ans_nostr.Call_S + "][" + ans_nostr.Call_T + "][" + ans_nostr.Call_R + "]");

                        switch (sw_press)
                        {
                            case CALLED.NO:
                                Debug.Log("NO:cnt_now=" + cnt_now + "/ans=" + ans_nostr.Call_NO);
                                BTN_NO[called_nostr[cnt_now].Call_NO].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                                if (called_nostr[cnt_now].Call_NO + 1 == now_nostr.Call_NO)
                                {

                                }
                                else
                                {
                                    sw_over = true;
                                }
                                break;
                            case CALLED.S:
                                BTN_S[called_nostr[cnt_now].Call_S].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                                if (called_nostr[cnt_now].Call_S + 1 == now_nostr.Call_S)
                                {

                                }
                                else
                                {
                                    sw_over = true;
                                }
                                break;
                            case CALLED.T:
                                BTN_T[called_nostr[cnt_now].Call_T].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                                if (called_nostr[cnt_now].Call_T + 1 == now_nostr.Call_T)
                                {

                                }
                                else
                                {
                                    sw_over = true;
                                }
                                break;
                            case CALLED.R:
                                BTN_R[called_nostr[cnt_now].Call_R].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                                if (called_nostr[cnt_now].Call_R + 1 == now_nostr.Call_R)
                                {
                                    count = -1;
                                    cnt_now++;
                                    if (called_nostr.Count <= cnt_now)
                                    {
                                        mode = MODE.NOSTR;
                                    }
                                    else
                                    {
                                        BTN_NO[0].enabled = true;
                                        BTN_NO[1].enabled = true;
                                    }
                                }
                                else
                                {
                                    sw_over = true;
                                }
                                break;
                        }
                        if (sw_over == true)
                        {
                            BTN_START.gameObject.SetActive(true);
                            MSG_CLICK_BUTTON.enabled = true;
                            MSG_GAMEOVER.enabled = true;
                            MSG_POST_NOSTR.enabled = true;
                            BTN_NO[0].enabled = false;
                            BTN_NO[1].enabled = false;
                            BTN_S[0].enabled = false;
                            BTN_S[1].enabled = false;
                            BTN_T[0].enabled = false;
                            BTN_T[1].enabled = false;
                            BTN_R[0].enabled = false;
                            BTN_R[1].enabled = false;
                            mode = MODE.OVER;
                            msg_nostter = "TOTAL NOSTR = " + cnt_score + " / Play >> https://howto-nostr.info/CallNostr/";
                            BTN_POST.gameObject.SetActive(true);
                            count = -1;
                        }
                    }
                }
                break;
            case MODE.NOSTR:
                if (count == 0)
                {
                    MSG_NOSTR.text += "NOSTR ";
                    cnt_score++;
                }
                else if (count == 60)
                {
                    BTN_NO[0].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_NO[1].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_S[0].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_S[1].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_T[0].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_T[1].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_R[0].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_R[1].gameObject.GetComponent<FlashButtonCtrl>().Flash();
                    BTN_NO[0].enabled = false;
                    BTN_NO[1].enabled = false;
                    BTN_S[0].enabled = false;
                    BTN_S[1].enabled = false;
                    BTN_T[0].enabled = false;
                    BTN_T[1].enabled = false;
                    BTN_R[0].enabled = false;
                    BTN_R[1].enabled = false;
                    BTN_NO[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_NO[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_S[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_S[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_T[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_T[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_R[0].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BTN_R[1].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else if (count >= 120)
                {
                    count = -1;
                    cnt_now = 0;
                    mode = MODE.CALL;
                }
                break;
        }


        count++;
    }


    public void PressPostButton()
    {
        BTN_POST.gameObject.SetActive(false);
        if (msg_nostter != "")
        {
            Application.OpenURL("https://nostter.vercel.app/post?content=" + msg_nostter);
        }
    }

    public void PressStartButton()
    {
        BTN_START.gameObject.SetActive(false);
        BTN_POST.gameObject.SetActive(false);
        MSG_CLICK_BUTTON.enabled = false;
        MSG_GAMEOVER.enabled = false;
        MSG_NOSTR.text = "";
        mode = MODE.CALL;
        called_nostr.Clear();
        cld = CALLED.NO;
        cnt_now = 0;
        cnt_score = 0;
        cnt_wait_max = CNT_WAIT_MAX_DEFAULT;
        cnt_wait = -1;
        count = -1;
        sw_over = false;
	}
    public void PressNO1Button()
    {
        sw_press = CALLED.NO;
        sw_button = true;
        BTN_NO[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_NO[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_NO = 1;
        BTN_NO[0].enabled = false;
        BTN_NO[1].enabled = false;
        BTN_S[0].enabled = true;
        BTN_S[1].enabled = true;
	}
    public void PressNO2Button()
    {
        sw_press = CALLED.NO;
        sw_button = true;
        BTN_NO[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_NO[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_NO = 2;
        BTN_NO[0].enabled = false;
        BTN_NO[1].enabled = false;
        BTN_S[0].enabled = true;
        BTN_S[1].enabled = true;
    }
    public void PressS1Button()
    {
        sw_press = CALLED.S;
        sw_button = true;
        BTN_S[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_S[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_S = 1;
        BTN_S[0].enabled = false;
        BTN_S[1].enabled = false;
        BTN_T[0].enabled = true;
        BTN_T[1].enabled = true;
        sw_button = true;
    }
    public void PressS2Button()
    {
        sw_press = CALLED.S;
        sw_button = true;
        BTN_S[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_S[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_S = 2;
        BTN_S[0].enabled = false;
        BTN_S[1].enabled = false;
        BTN_T[0].enabled = true;
        BTN_T[1].enabled = true;
        sw_button = true;
    }
    public void PressT1Button()
    {
        sw_press = CALLED.T;
        sw_button = true;
        BTN_T[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_T[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_T = 1;
        BTN_T[0].enabled = false;
        BTN_T[1].enabled = false;
        BTN_R[0].enabled = true;
        BTN_R[1].enabled = true;
        sw_button = true;
    }
    public void PressT2Button()
    {
        sw_press = CALLED.T;
        sw_button = true;
        BTN_T[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_T[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_T = 2;
        BTN_T[0].enabled = false;
        BTN_T[1].enabled = false;
        BTN_R[0].enabled = true;
        BTN_R[1].enabled = true;
        sw_button = true;
    }
    public void PressR1Button()
    {
        sw_press = CALLED.R;
        sw_button = true;
        BTN_R[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_R[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_R = 1;
        BTN_R[0].enabled = false;
        BTN_R[1].enabled = false;
        sw_button = true;
    }
    public void PressR2Button()
    {
        sw_press = CALLED.R;
        sw_button = true;
        BTN_R[0].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        BTN_R[1].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.2f);
        now_nostr.Call_R = 2;
        BTN_R[0].enabled = false;
        BTN_R[1].enabled = false;
        sw_button = true;
    }
}
