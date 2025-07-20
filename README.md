# Challenge_ATM

Aclaraciones:
- Dentro de Challenge_ATM_API/appsetting.json debe cargarse la dirección personal de la base de datos.

- Agregué la opción de depósito para poder tener dinero en cuenta.

- Hay una seed creada con un banco, un usuario y una tarjeta, que se carga al correr las migraciones de BDD.

- La tarjeta creada en la seed es:
111-1111-1111-1111: la cual en BDD se guarda "1111111111111111"
PIN: 1234

- La creación de usuario crea una tarjeta con un numero de 16 caracteres random, al igual que un pin de 4 digitos random.
- Al crearlo, debe guardar el pin. No estaba dentro de las tareas el front de eso, pero se puede hacer por swagger

- La configuración de mi navegador acepta la coma en los inputs de numero, sin embargo, utilicé un input tipo texto
en el retiro/depósito del dinero. Espero funcione en su local o donde corra el proyecto.

Seguridad:
- El pin se guarda como hash en la BDD utilizando el Identity, garantizando la privacidad de la persona que carga su PIN.
- La sesión es por un token en JWT, para utilizarlo en swagger no se olvide de escribir "Bearer ..." y el token obtenido.
