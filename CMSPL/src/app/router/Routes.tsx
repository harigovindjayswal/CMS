import { createBrowserRouter, Navigate } from "react-router";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ClientDashboard from "../../features/clients/dashboards/ClientDashboard";
import ClientForm from "../../features/clients/form/ClientForm";
import ClientDetailsPage from "../../features/clients/details/ClientDetailsPage";
import Counter from "../../features/counter/Counter";
import TestErrors from "../../features/errors/TestErrors";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import LoginForm from "../../features/account/LoginForm";
import RegisterForm from "../../features/account/RegisterForm";
import RequireAuth from "./RequireAuth";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        element: <RequireAuth />,
        children: [
          { path: "clients", element: <ClientDashboard /> },
          { path: "clientDetails/:id", element: <ClientDetailsPage /> },
          { path: "createClient", element: <ClientForm key={"Create"} /> },
          { path: "manage/:id", element: <ClientForm /> },
        ],
      },
      { path: "", element: <HomePage /> },
      { path: "counter", element: <Counter /> },
      { path: "login", element: <LoginForm /> },
      { path: "register", element: <RegisterForm /> },
      { path: "errors", element: <TestErrors /> },
      { path: "not-found", element: <NotFound /> },
      { path: "server-error", element: <ServerError /> },
      { path: "*", element: <Navigate replace to="/not-found" /> },
    ],
  },
]);
