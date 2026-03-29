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
import RequireRole from "./RequireRole";
import MyProfilePage from "../../features/profile/pages/MyProfilePage";
import MyLawyerRequestsPage from "../../features/lawyerRequests/pages/MyLawyerRequestsPage";
import IncomingLawyerRequestsPage from "../../features/lawyerRequests/pages/IncomingLawyerRequestsPage";
import RequestLawyerPage from "../../features/lawyerRequests/pages/RequestLawyerPage";
import CasesPage from "../../features/cases/pages/CasesPage";
import CaseDetailsPage from "../../features/cases/pages/CaseDetailsPage";
import CreateCasePage from "../../features/cases/pages/CreateCasePage";
import DashboardPage from "../../features/dashboard/DashboardPage";
import StatesPage from "../../features/admin/masterData/pages/StatesPage";
import DistrictsPage from "../../features/admin/masterData/pages/DistrictsPage";
import CitiesPage from "../../features/admin/masterData/pages/CitiesPage";
import CaseTypesPage from "../../features/admin/masterData/pages/CaseTypesPage";
import CourtTypesPage from "../../features/admin/masterData/pages/CourtTypesPage";
import CourtsPage from "../../features/admin/masterData/pages/CourtsPage";
import RegisterManagedClientPage from "../../features/lawyerAdmin/pages/RegisterManagedClientPage";
import RegisterManagedLawyerPage from "../../features/lawyerAdmin/pages/RegisterManagedLawyerPage";
import ManagedClientsPage from "../../features/lawyerAdmin/pages/ManagedClientsPage";
import ManagedLawyersPage from "../../features/lawyerAdmin/pages/ManagedLawyersPage";
import ManagedCasesPage from "../../features/lawyerAdmin/pages/ManagedCasesPage";
import CreateManagedCasePage from "../../features/lawyerAdmin/pages/CreateManagedCasePage";
import LawyerAdminCaseDetailsPage from "../../features/lawyerAdmin/pages/LawyerAdminCaseDetailsPage";
import RegisterManagedStaffPage from "../../features/lawyerAdmin/pages/RegisterManagedStaffPage";
import ManagedStaffPage from "../../features/lawyerAdmin/pages/ManagedStaffPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        element: <RequireAuth />,
        children: [
          { path: "dashboard", element: <DashboardPage /> },

          {
            element: <RequireRole roles={["Admin", "LawyerAdmin"]} />,
            children: [
              { path: "clients", element: <ClientDashboard /> },
              { path: "clientDetails/:id", element: <ClientDetailsPage /> },
              { path: "createClient", element: <ClientForm key={"Create"} /> },
              { path: "manage/:id", element: <ClientForm /> },
            ],
          },

          {
            element: <RequireRole roles={["Client", "Lawyer", "Staff"]} />,
            children: [{ path: "profile", element: <MyProfilePage /> }],
          },

          {
            element: <RequireRole roles={["Client"]} />,
            children: [
              { path: "my-requests", element: <MyLawyerRequestsPage /> },
              { path: "request-lawyer", element: <RequestLawyerPage /> },
            ],
          },

          {
            element: <RequireRole roles={["Lawyer"]} />,
            children: [
              { path: "incoming-requests", element: <IncomingLawyerRequestsPage /> },
              { path: "cases/create/:lawyerRequestId", element: <CreateCasePage /> },
            ],
          },

          {
            element: <RequireRole roles={["Client", "Lawyer", "Staff"]} />,
            children: [
              { path: "cases", element: <CasesPage /> },
              { path: "cases/:id", element: <CaseDetailsPage /> },
            ],
          },

          {
            element: <RequireRole roles={["Admin"]} />,
            children: [
              { path: "admin/master/states", element: <StatesPage /> },
              { path: "admin/master/districts", element: <DistrictsPage /> },
              { path: "admin/master/cities", element: <CitiesPage /> },
              { path: "admin/master/case-types", element: <CaseTypesPage /> },
              { path: "admin/master/court-types", element: <CourtTypesPage /> },
              { path: "admin/master/courts", element: <CourtsPage /> },
            ],
          },

          {
            element: <RequireRole roles={["LawyerAdmin"]} />,
            children: [
              { path: "lawyeradmin/register-client", element: <RegisterManagedClientPage /> },
              { path: "lawyeradmin/register-lawyer", element: <RegisterManagedLawyerPage /> },
              { path: "lawyeradmin/register-staff", element: <RegisterManagedStaffPage /> },
              { path: "lawyeradmin/clients", element: <ManagedClientsPage /> },
              { path: "lawyeradmin/lawyers", element: <ManagedLawyersPage /> },
              { path: "lawyeradmin/staff", element: <ManagedStaffPage /> },
              { path: "lawyeradmin/cases", element: <ManagedCasesPage /> },
              { path: "lawyeradmin/cases/:id", element: <LawyerAdminCaseDetailsPage /> },
              { path: "lawyeradmin/cases/create", element: <CreateManagedCasePage /> },
            ],
          },
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
