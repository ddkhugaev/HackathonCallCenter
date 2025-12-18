using HackathonCallCenter.Models;
using System.Text;

namespace HackathonCallCenter.Helpers
{
    public class DialogueParser
    {
        /// <summary>
        /// Преобразует текстовый диалог с ролями в список DialogueLine
        /// </summary>
        /// <param name="dialogueText">Текст диалога с указанием ролей через двоеточие</param>
        /// <returns>Список реплик с ролями</returns>
        public static List<DialogueLine> ParseDialogue(string dialogueText)
        {
            var dialogueLines = new List<DialogueLine>();

            if (string.IsNullOrWhiteSpace(dialogueText))
                return dialogueLines;

            var lines = dialogueText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            string currentRole = null;
            StringBuilder currentText = new StringBuilder();

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                // Проверяем, начинается ли строка с указания роли
                if (trimmedLine.ToLower().StartsWith("оператор:") || trimmedLine.ToLower().StartsWith("клиент:"))
                {
                    // Сохраняем предыдущую реплику, если она есть
                    if (currentRole != null && currentText.Length > 0)
                    {
                        dialogueLines.Add(new DialogueLine(
                            currentRole,
                            currentText.ToString().Trim()
                        ));
                        currentText.Clear();
                    }

                    // Определяем роль и начало текста
                    if (trimmedLine.ToLower().StartsWith("оператор:"))
                    {
                        currentRole = "Оператор";
                        currentText.Append(trimmedLine.Substring(9).Trim()); // удаляем "оператор:"
                    }
                    else if (trimmedLine.ToLower().StartsWith("клиент:"))
                    {
                        currentRole = "Клиент";
                        currentText.Append(trimmedLine.Substring(7).Trim()); // удаляем "клиент:"
                    }
                }
                else
                {
                    // Если строка не начинается с роли, это продолжение предыдущей реплики
                    if (currentRole != null)
                    {
                        currentText.Append(" ").Append(trimmedLine);
                    }
                }
            }

            // Добавляем последнюю реплику
            if (currentRole != null && currentText.Length > 0)
            {
                dialogueLines.Add(new DialogueLine (
                    currentRole,
                    currentText.ToString().Trim()
                ));
            }

            return dialogueLines;
        }

        /// <summary>
        /// Альтернативный метод с более простой логикой (если каждая строка начинается с роли)
        /// </summary>
        public static List<DialogueLine> ParseDialogueSimple(string dialogueText)
        {
            var dialogueLines = new List<DialogueLine>();

            if (string.IsNullOrWhiteSpace(dialogueText))
                return dialogueLines;

            var lines = dialogueText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                // Определяем роль
                if (trimmedLine.StartsWith("оператор:"))
                {
                    var text = trimmedLine.Substring(9).Trim();
                    dialogueLines.Add(new DialogueLine("Оператор", text));
                }
                else if (trimmedLine.StartsWith("клиент:"))
                {
                    var text = trimmedLine.Substring(7).Trim();
                    dialogueLines.Add(new DialogueLine("Клиент", text));
                }
            }

            return dialogueLines;
        }
    }
}
