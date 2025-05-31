using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("Dialogue Settings")]
    [SerializeField] private GameObject dialoguePanel; // 对话面板
    [SerializeField] private Text speakerName; // 说话者名字文本
    [SerializeField] private Text content; // 对话内容文本
    [SerializeField] private Image portraitImage; // 头像图片
    [SerializeField] private Button optionButtonPrefab; // 选项按钮
    [SerializeField] private Transform optionsPanel;
    private Queue<DialogueLine> currentLines;
    private DialogueData currentDialogue;
    private bool isInDialogue = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (isInDialogue) return;

        currentDialogue = dialogue;
        currentLines = new Queue<DialogueLine>(dialogue.lines);
        isInDialogue = true;

        dialoguePanel.SetActive(true);
        DisplayNext();
    }

    public void DisplayNext()
    {
        if (currentLines.Count == 0)
        {
            if (currentDialogue.hasChoice && currentDialogue.options.Count > 0)
            {
                ShowOptions();
                return;
            }

            EndDialogue();
            return;
        }

        DialogueLine line = currentLines.Dequeue();
        speakerName.text = line.speakerName;
        content.text = line.content;
        portraitImage.sprite = line.portrait; // 设置头像图片
        portraitImage.enabled = line.portrait != null; // 如果没有头像，隐藏 Image 组件

        line.onLineEndEvent?.Invoke();
    }

    private void ShowOptions()
    {
        // 清除现有选项
        foreach (Transform child in optionsPanel)
        {
            Destroy(child.gameObject);
        }

        // 创建新选项按钮
        for (int i = 0; i < currentDialogue.options.Count; i++)
        {
            ChoiceOption option = currentDialogue.options[i];
            Button optionButton = Instantiate(optionButtonPrefab, optionsPanel);
            // 使用 Text 组件显示选项文本
            optionButton.GetComponentInChildren<Text>().text = option.optionText;

            int index = i; // 闭包捕获
            optionButton.onClick.AddListener(() => ChooseOption(index));
        }
    }

    public void ChooseOption(int index)
    {
        if (index < 0 || index >= currentDialogue.options.Count) return;

        // 清除选项
        foreach (Transform child in optionsPanel)
        {
            Destroy(child.gameObject);
        }

        // 触发选项事件
        currentDialogue.options[index].onChooseEvent?.Invoke();

        // 继续下一段对话或结束
        if (currentDialogue.options[index].nextDialogue != null)
        {
            StartDialogue(currentDialogue.options[index].nextDialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        portraitImage.enabled = false; // 隐藏头像
        isInDialogue = false;
    }

    public bool IsInDialogue() => isInDialogue;
}