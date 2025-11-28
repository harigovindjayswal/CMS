import { createBrowserRouter } from "react-router";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ClientDashboard from "../../features/clients/dashboards/ClientDashboard";
import ClientForm from "../../features/clients/form/ClientForm";
import ClientDetailsPage from "../../features/clients/details/ClientDetailsPage";
import Counter from "../../features/counter/Counter";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "clients", element: <ClientDashboard /> },
      { path: "clientDetails/:id", element: <ClientDetailsPage /> },
      { path: "createClient", element: <ClientForm key={"Create"} /> },
      { path: "manage/:id", element: <ClientForm /> },
      { path: "counter", element: <Counter /> },
    ],
  },
]);
