# Instalacion

Ejecutamos los siguientes comandos

```shell
npm install
```

# Estructura de carpetas

Modelo propuesto por ChatGPT

src/
├── api/               
│   ├── axiosInstance.ts
│   ├── appointmentService.ts
│   └── userService.ts
├── assets/           
│   ├── images/
│   └── icons/
├── components/        
│   ├── common/       
│   ├── layout/       
│   └── appointment/   
├── config/            
│   └── env.ts        
├── context/          
│   └── AuthContext.tsx
├── hooks/            
│   ├── useAuth.ts
│   └── useFetch.ts
├── pages/             
│   ├── Home/
│   │   └── HomePage.tsx
│   ├── Appointments/
│   │   └── AppointmentPage.tsx
│   └── Login/
│       └── LoginPage.tsx
├── redux/            
│   ├── store.ts
│   ├── slices/
│   │   ├── authSlice.ts
│   │   └── appointmentSlice.ts
├── routes/           
│   └── AppRouter.tsx
├── styles/            
│   ├── variables.scss 
│   └── global.scss    
├── types/             
│   ├── Appointment.ts
│   └── User.ts
├── utils/             
│   ├── formatDate.ts
│   └── validateForm.ts
├── App.tsx            
├── index.tsx          
└── vite-env.d.ts     


## Descripción de Carpetas

### api/
Contiene las configuraciones para las llamadas a la API, como instancias de Axios o servicios específicos. Mantén aquí la lógica para interactuar con el backend.

### assets/
Almacena recursos estáticos como imágenes, íconos o fuentes.

### components/
Componentes reutilizables y específicos de funcionalidad.

common/: Botones, inputs, selectores, etc.
layout/: Header, Footer, Sidebar.

Funcionales: Agrupa componentes relacionados con una funcionalidad específica (ejemplo: citas).

### config/
Configuraciones globales como variables de entorno o constantes.

### context/
Contextos para manejar estados globales como autenticación o tema.

### hooks/
Hooks personalizados para encapsular lógica reutilizable.

### pages/
Páginas principales asociadas a las rutas de la aplicación.

### redux/
Configuración de Redux (si es utilizado). Define aquí el estado global de la aplicación.

### routes/
Define la configuración de rutas, incluyendo rutas protegidas.

### styles/
Archivos de estilos globales y variables para mantener consistencia visual.

### types/
Define los tipos y modelos de datos para TypeScript.

### utils/
Funciones auxiliares reutilizables en toda la aplicación.


# Librerias que usamos

### React Router Dom

Usada para la navegación.

### Axios

Usada para la comunicación con nuestro backend