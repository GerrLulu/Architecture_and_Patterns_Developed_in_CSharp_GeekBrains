# Architecture_and_Patterns_Developed_in_CSharp_GeekBrains
## Home work for GeekBrains

### Урок 1. Введение в паттерны. Что это такое. Паттерны и антипаттерны

#### 1. Проанализировать ранее написанные проекты на наличие антипаттернов
#### 2. Расписать зачем нужна архитектура в проекте
#### 3. *Есть файл с программой, его нужно отрефакторить
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте вас приветствует математическая программа");
            Console.WriteLine("пажалуйста введите число. ");

            String S = Console.ReadLine();

            if (S == "q"){
               return;
            }
            
            int M = Int32.Parse(S);
            int c1 = 1; int c2 = 0;
            int c3 = 0;
            
            for (int i = 1; i <= M; i++)
            {
                c1 = c1*i;
                c2 = c2 + i;
                
                if (i%2 == 0)
                {
                    c3 = i;
                }
            }
            
            Console.WriteLine("Факториал равет " + c1); Console.WriteLine("Сума от 1 до N равна " + c2);
            Console.WriteLine("максимальное четное число меньше N равно" + c3);
            Console.ReadLine();
        }
    }

### Урок 2. Основные шаблоны

#### 1. Разнести класс Player на логические модули
#### 2. Отделить отображение от логики
#### 3. Переделать движение корабля через физику
