import { createBrowserRouter } from "react-router";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ClientDashboard from "../../features/clients/dashboards/ClientDashboard";
import ClientForm from "../../features/clients/form/ClientForm";
import ClientDetails from "../../features/clients/details/ClientDetails";

export const router=createBrowserRouter([
    {
        path:'/',
        element: <App />,
        children:[
            {path:'',element:<HomePage />},
            {path:'clients',element:<ClientDashboard />},
            {path:'clientDetails/:id',element:<ClientDetails />},
            {path:'createClient',element:<ClientForm key={'Create'} />},
            {path:'manage/:id',element:<ClientForm />},
        ]
    }
])