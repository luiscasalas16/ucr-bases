# Herencia

En Entity Framework, la herencia se refiere a cómo se representan las relaciones de herencia de un modelo de dominio orientado a objetos en una base de datos relacional. Las tres principales estrategias de mapeo de herencia son:

- **Tabla por jerarquía (TPH)**: todas las entidades de la jerarquía de herencia se almacenan en una sola tabla.
- **Tabla por tipo (TPT)**: cada tipo de entidad en la jerarquía de herencia se almacena en su propia tabla.
- **Tabla por clase concreta (TPC)**: cada clase de entidad concreta (no abstracta) tiene su propia tabla, sin tabla para clases base abstractas.

Ejemplos de implementación:

- **Tabla por jerarquía (TPH)**: [EfInheritanceTpH](./EfInheritanceTpH)
- **Tabla por tipo (TPT)**: [EfInheritanceTpT](./EfInheritanceTpT)
- **Tabla por clase concreta (TPC)**: [EfInheritanceTpC](./EfInheritanceTpC)

## Tabla por jerarquía (TPH)

- **Características**:
  - Tabla única : todas las entidades de la jerarquía de herencia se almacenan en una única tabla de base de datos.
  - Columna discriminadora: esta columna diferencia entre los tipos de entidad. Por ejemplo, una columna “PaymentType” puede indicar si un registro corresponde a un pago con tarjeta, un pago UPIPayment o un pago con efectivo en el momento de la entrega.
  - Columnas que aceptan valores nulos: las propiedades específicas de los tipos derivados pueden generar valores nulos en la base de datos para otros tipos de entidades
- **Ventajas**:
  - Esquema simplificado: una sola tabla para administrar todos los tipos de pago.
  - Mantenimiento más fácil: agregar un nuevo tipo de pago implica crear una nueva clase y actualizar la configuración del modelo.
  - Rendimiento en operaciones de lectura: minimizar las uniones puede mejorar el rendimiento de lectura.
- **Desventajas**:
  - Valores nulos: las columnas específicas de un tipo de pago son nulas para otros tipos.
  - Restricciones de la base de datos: es más difícil aplicar restricciones NOT NULL en propiedades de tipos derivados.
  - Escasez de tablas: a medida que se agregan más tipos derivados con propiedades únicas, la tabla Pagos puede acumular numerosas columnas que aceptan valores nulos, lo que genera escasez. Esto genera un desperdicio de espacio de almacenamiento y una degradación del rendimiento debido al aumento del ancho de la tabla.
  - Gastos generales del discriminador: para determinar el tipo de entidad, cada consulta debe evaluar la columna del discriminador (PaymentType). El procesamiento adicional puede afectar el rendimiento de la consulta, especialmente en conjuntos de datos grandes.

## Tabla por tipo (TPT)

- **Características**:
  - Tablas separadas: cada entidad en la jerarquía de herencia se almacena en su propia tabla de base de datos.
  - Relaciones de clave externa: Las tablas de clases derivadas tienen una clave principal que también es una clave externa que hace referencia a la clave principal de la tabla base, formando una relación uno a uno.
  - Sin columnas nulas: dado que cada tabla contiene solo las propiedades de esa clase, no hay columnas nulas debido al esquema normalizado en comparación con TPH.
- **Ventajas**:
  - Esquema de base de datos normalizado: TPT promueve un diseño normalizado al separar las propiedades comunes y específicas en diferentes tablas, lo que reduce la redundancia.
  - Sin columnas nulas: cada tabla contiene solo las propiedades relevantes para ese tipo de entidad, eliminando las columnas nulas.
  - Integridad de los datos: es más fácil aplicar restricciones de base de datos como NOT NULL en propiedades de tipo derivadas.
  - Separación lógica: separación clara de datos para diferentes tipos de entidades, lo que puede mejorar la organización y la integridad de los datos.
- **Desventajas**:
  - Sobrecarga de rendimiento: la recuperación de entidades derivadas requiere uniones entre tablas, lo que puede degradar el rendimiento, especialmente con conjuntos de datos grandes o jerarquías de herencia profundas.
  - Complejidad en las consultas: Las consultas SQL generadas son más complejas debido a las uniones, lo que hace que la depuración y la optimización sean más desafiantes.
  - Mayor mantenimiento: administrar varias tablas puede aumentar la complejidad de las tareas de mantenimiento de la base de datos.

## Tabla por clase concreta (TPC)

- **Características**:
  - Tablas separadas para cada clase concreta: cada clase concreta en la jerarquía se asigna a su propia tabla.
  - Representación de entidad completa: cada tabla representa completamente una entidad concreta, incluidas las propiedades heredadas.
  - Sin tabla para clase base abstracta: La clase base no está representada por una tabla separada.
  - No se requieren uniones: las consultas contra tipos derivados no requieren uniones, lo que mejora potencialmente el rendimiento.
  - Sin relaciones de clave externa: a diferencia de TPT, no existen relaciones ni claves externas entre las clases base y las derivadas.
  - Sin columna discriminadora: a diferencia de TPH, TPC no requiere una columna discriminadora para identificar el tipo de entidad.
  - Sin columnas nulas: a diferencia de Tabla por jerarquía (TPH), TPC evita las columnas nulas al garantizar que cada tabla solo contenga columnas relevantes para su entidad.
- **Ventaja**:
  - Beneficios de rendimiento: dado que todas las propiedades están en una tabla por clase concreta, las consultas no requieren uniones, lo que puede mejorar el rendimiento.
  - Simplicidad: el esquema de la base de datos es sencillo, con una tabla por clase concreta.
  - Sin columnas nulas: a diferencia de TPH, no hay columnas nulas porque cada tabla contiene solo propiedades relevantes.
  - Fácil mantenimiento: las tablas separadas facilitan la gestión de índices y restricciones específicos de cada tipo de entidad.
- **Desventajas**:
  - Redundancia de datos: las propiedades comunes heredadas de la clase base se repiten en la tabla de cada clase derivada, lo que genera redundancia.
  - Cambios de esquema: agregar una nueva propiedad a la clase base requiere actualizar todas las tablas derivadas.
  - Soporte limitado para consultas polimórficas: las consultas que necesitan recuperar datos en toda la jerarquía de herencia pueden volverse más complejas ya que necesitan unir resultados de múltiples tablas.

## Resumen

- **Rendimiento**: TPH generalmente ofrece un mejor rendimiento debido a consultas más simples.
- **Complejidad del esquema**: TPH da como resultado un esquema más simple, mientras que TPT y TPC pueden aumentar la complejidad.
- **Integridad de los datos**: TPT proporciona una mejor integridad de los datos gracias a las tablas normalizadas.
- **Redundancia de datos**: TPC puede generar redundancia de datos y mayores requisitos de almacenamiento.

## Referencias

- <https://dotnettutorials.net/lesson/entity-framework-core-inheritance/>
- <https://dotnettutorials.net/lesson/table-per-hierarchy-inheritance-in-entity-framework-core/>
- <https://dotnettutorials.net/lesson/table-per-type-inheritance-in-entity-framework-core/>
- <https://dotnettutorials.net/lesson/table-per-concrete-type-inheritance-in-entity-framework-core/>
