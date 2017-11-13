using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Serializable]
    public enum WmCurrency
    {
        None = 0,
        Z = 1, // Доллар
        E, // Евро
        R, // Рубль
        U, // Гривна
        B, // Рубль РБ
        G, // Золото
        D, // Мне должны
        C, // Я должен
        X, // Bitcoin

        // Только в Mini
        K, // Казахский тенге
        V, // Вьетнамский донг

        // (нельзя создать)
        H, // Bitcoin Cash (BCH)

        // Отключено.
        Y = 100, // Узбекский сум
        L = 101, // Турецкая лира

        // Не используется!
        A = 200,
        F,
        I,
        J,
        M,
        N,
        O,
        P,
        Q,
        S,
        T,
        W
    }
}