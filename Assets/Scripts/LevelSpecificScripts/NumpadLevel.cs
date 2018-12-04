using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumpadLevel : MonoBehaviour
{

    public LoadNextLevel manager;

    public VRIO_Button[] Buttons = new VRIO_Button[10];

    int index = 0;

    bool holding = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
        {
            holding = false;
            foreach (VRIO_Button button in Buttons)
            {
                if (button.pressed)
                {
                    holding = true;
                }
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    if (Buttons[6].pressed)
                    {
                        index += 1;
                        holding = true;
                        print("6");
                    }
                    break;

                case 1:
                    if (Buttons[9].pressed)
                    {
                        index += 1;
                        holding = true;
                        print("9");
                    }
                    else
                    {
                        foreach (VRIO_Button button in Buttons)
                        {
                            if (button.pressed)
                            {
                                index = 0;
                                break;
                            }
                        }
                    }
                    break;

                case 2:
                    if (Buttons[4].pressed)
                    {
                        index += 1;
                        holding = true;
                        print("4");
                    }
                    else
                    {
                        foreach (VRIO_Button button in Buttons)
                        {
                            if (button.pressed)
                            {
                                index = 0;
                                break;
                            }
                        }
                    }
                    break;

                case 3:
                    if (Buttons[2].pressed)
                    {
                        index += 1;
                        holding = true;
                        print("2");
                    }
                    else
                    {
                        foreach (VRIO_Button button in Buttons)
                        {
                            if (button.pressed)
                            {
                                index = 0;
                                break;
                            }
                        }
                    }
                    break;

                case 4:
                    if (Buttons[0].pressed)
                    {
                        manager.NextLevel("Level3");
                        print("0");
                    }
                    else
                    {
                        foreach (VRIO_Button button in Buttons)
                        {
                            if (button.pressed)
                            {
                                index = 0;
                                break;
                            }
                        }
                    }
                    break;

            }
        }
    }
}
