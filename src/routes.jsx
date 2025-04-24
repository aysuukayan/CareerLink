import { Home, Profile, SignIn, SignUp, WelcomeScreen } from "@/pages";  // WelcomeScreen bileşenini import et

export const routes = [
  {
    name: "anasayfa",
    path: "/home",
    element: <Home />,

  },
  {
    name: "profil",
    path: "/profile",
    element: <Profile />,

  },
  {
    name: "Giriş Yap",
    path: "/sign-in",
    element: <SignIn />,

  },
  {
    name: "Kayıt Ol",
    path: "/sign-up",
    element: <SignUp />,

  },
  {
    name: "İş İlanları",
    href: "https://www.material-tailwind.com/docs/react/installation",
    target: "_blank",
    element: "",
  },
  // WelcomeScreen'i '/' rotasına ekle
  {
    
    path: "/",
    element: <WelcomeScreen />,  // Buraya WelcomeScreen bileşenini ekledik
  },
];

export default routes;
