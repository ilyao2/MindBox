# Тестовое задание в MindBox

## Задание 1

Напишите на C# библиотеку для поставки внешним клиентам,
которая умеет вычислять площадь круга по радиусу и треугольника по трем сторонам.

Дополнительно:
- Легкость добавления других фигур
- Вычисление площади фигуры без знания типа фигуры в compile-time
- Проверку на то, является ли треугольник прямоугольным
- Юнит-тесты

### Ход выполнения
В задании отсутствует полный контекст, в котором будет использоваться библиотека,
поэтому будем рассматривать её, как библиотека, которая предоставлять интерфейс фигуры,
у которой можно рассчитать площадь.

Соответственно создаём интерфейс [IShape](Shapes/IShape.cs) с readonly свойством получения площади.

И классы, которые его реализуют: [Circle](Shapes/Circle.cs) и [Triangle](Shapes/Triangle.cs).

Допустим, что нам нужны закрытые для изменения объекты, после создания которых мы можем вычислить их площадь.
Исходя из того, что фигуры неизменямы, можно сразу сделать вывод, что площадь можно рассчитать один раз и дальше выводить рассчитанную.
Для оптимизации создания фигуры, можно использовать паттерн Lazy, чтобы площадь в первый раз рассчитывалась только при первой попытке её получить.

Для [Circle](Shapes/Circle.cs) важно учесть, что радиус круга должен обязательно быть больше 0.
Для [Triangle](Shapes/Triangle.cs) важно учесть, что все вершины не лежат на одной прямой.

Для работы с вершинами будем пользоваться простой неизменяемой структурой [Point](Shapes/Point.cs), которая хранит координаты.


Дополним [Triangle](Shapes/Triangle.cs) дополнительным readonly свойством `IsRight`,
которое аналагично площади рассчитается и покажет является ли треугольник прямоугольным.

Для проверки правильности работы полученных классов [Circle](Shapes/Circle.cs) и [Triangle](Shapes/Triangle.cs) будут использоваться Юнит-тесты.

### Итоги

- Реализована библиотека классов, которая содержит в себе:
  - [IShape](Shapes/IShape.cs) - интерфейс для создания новых фигур, у которых можно рассчитать площадь.
  - [Circle](Shapes/Circle.cs) - фигура круг, которая создаётся по радиусу. Усеет вычислять свою площадь.
  - [Triangle](Shapes/Triangle.cs) - фигура треугольник, которая создаётся по трём точкам.
  Умеет вычислять свою площадь и определять является ли треугольник прямоугольным.

- Для проверки правильности работы классы покрыты юнит-тестами.
- Для добавления новой фигуры, нужно всего-лишь реализовать интерфейс IShape.
- Вычислить площадь фигуры не зная её тип в compaile-time можно через свойство Area.
Достаточно лишь знать, что фигура реализовала интерфейс [IShape](Shapes/IShape.cs).


## Задание 2

В базе данных MS SQL Server есть продукты и категории.
Одному продукту может соответствовать много категорий,
в одной категории может быть много продуктов.
Напишите SQL запрос для выбора всех пар «Имя продукта – Имя категории».
Если у продукта нет категорий, то его имя все равно должно выводиться.

### Ход работы

В задании не указан контекст. Не указано как выглядит схема БД.
Представим, что она выглядит следующим образом:

```
"Продукт" (
  "@Продукт" INT PRIMARY KEY,
  "Название" VARCHAR(100)
)

"Категория" (
  "@Категория" INT PRIMARY KEY,
  "Название" VARCHAR(100)
)

"ПродуктКатегория" (
  "Продукт" INT FOREIGN KEY,
  "Категория" INT FOREIGN KEY
)
```
Реализация отношения МногиеКоМногим в виде третьей нормальной формы БД.

Тогда решение будет выглядеть следующим образом: [task2.sql](task2.sql)

### Итоги

Уточнён контекст и написан [запрос](task2.sql).
