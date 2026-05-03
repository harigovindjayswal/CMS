// import { Navigate } from "react-router";
// import { useAccount } from "../../lib/hooks/useAccount";
// import { Typography } from "@mui/material";

// export default function DashboardPage() {
//   const { currentUser, loadingUserInfo } = useAccount();

//   if (loadingUserInfo) return <Typography>Loading...</Typography>;
//   if (!currentUser) return <Navigate to="/login" replace />;

//   switch (currentUser.userType) {
//     case "Client":
//       return <Navigate to="/request-lawyer" replace />;
//     case "Lawyer":
//       return <Navigate to="/incoming-requests" replace />;
//     case "LawyerAdmin":
//       return <Navigate to="/lawyeradmin/clients" replace />;
//     case "Admin":
//       return <Navigate to="/admin/master/states" replace />;
//     default:
//       return <Navigate to="/profile" replace />;
//   }
// }

import { Navigate } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";
import {
  Box,
  Grid,
  Paper,
  Typography,
  Skeleton,
} from "@mui/material";
import {
  Gavel,
  PendingActions,
  CheckCircle,
  Cancel,
  Group,
  Work,
  Assignment,
} from "@mui/icons-material";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
} from "recharts";

// -------------------
// 🔹 Stat Card
// -------------------
interface StatCardProps {
  title: string;
  value: number | string;
  icon: React.ReactNode;
  color: "primary" | "secondary" | "success" | "warning" | "error" | "info";
}
interface ChartProps {
  title: string;
}
function StatCard({ title, value, icon, color }:StatCardProps) {
  return (
    <Paper sx={{ p: 2.5, borderRadius: 3 }}>
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Box>
          <Typography variant="body2" color="text.secondary">
            {title}
          </Typography>
          <Typography variant="h5" fontWeight="bold">
            {value}
          </Typography>
        </Box>

        <Box
          sx={{
            p: 1.5,
            borderRadius: "50%",
            bgcolor: `${color}.light`,
            color: `${color}.main`,
            display: "flex",
          }}
        >
          {icon}
        </Box>
      </Box>
    </Paper>
  );
}

// -------------------
// 🔹 Loading Skeleton
// -------------------
function DashboardSkeleton() {
  return (
    <Grid container spacing={2}>
      {[1, 2, 3, 4].map((i) => (
        <Grid item xs={12} md={3} key={i}>
          <Skeleton variant="rounded" height={100} />
        </Grid>
      ))}
      <Grid item xs={12}>
        <Skeleton variant="rounded" height={250} />
      </Grid>
    </Grid>
  );
}

// -------------------
// 🔹 Chart Data (replace with API)
// -------------------
const chartData = [
  { name: "Jan", value: 10 },
  { name: "Feb", value: 20 },
  { name: "Mar", value: 15 },
  { name: "Apr", value: 30 },
];

// -------------------
// 🔹 Chart Component
// -------------------
function DashboardChart({ title }: ChartProps) {
  return (
    <Paper sx={{ mt: 3, p: 2.5, borderRadius: 3 }}>
      <Typography variant="h6" mb={2}>
        {title}
      </Typography>
      <ResponsiveContainer width="100%" height={250}>
        <BarChart data={chartData}>
          <XAxis dataKey="name" />
          <YAxis />
          <Tooltip />
          <Bar dataKey="value" />
        </BarChart>
      </ResponsiveContainer>
    </Paper>
  );
}

// -------------------
// 👤 Client Dashboard
// -------------------
function ClientDashboard() {
  return (
    <Box>
      <Typography variant="h5" mb={2} fontWeight="bold">
        Client Dashboard
      </Typography>

      <Grid container spacing={2}>
        <Grid item xs={12} md={3}>
          <StatCard title="Total Cases" value={12} icon={<Gavel />} color="primary" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Pending Requests" value={3} icon={<PendingActions />} color="warning" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Approved" value={7} icon={<CheckCircle />} color="success" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Rejected" value={2} icon={<Cancel />} color="error" />
        </Grid>
      </Grid>

      <DashboardChart title="Case Trends" />
    </Box>
  );
}

// -------------------
// ⚖️ Lawyer Dashboard
// -------------------
function LawyerDashboard() {
  return (
    <Box>
      <Typography variant="h5" mb={2} fontWeight="bold">
        Lawyer Dashboard
      </Typography>

      <Grid container spacing={2}>
        <Grid item xs={12} md={3}>
          <StatCard title="Incoming Requests" value={15} icon={<Assignment />} color="primary" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Active Cases" value={8} icon={<Gavel />} color="secondary" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Approved" value={10} icon={<CheckCircle />} color="success" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Pending" value={5} icon={<PendingActions />} color="warning" />
        </Grid>
      </Grid>

      <DashboardChart title="Workload Overview" />
    </Box>
  );
}

// -------------------
// 🛠️ Admin Dashboard
// -------------------
function AdminDashboard() {
  return (
    <Box>
      <Typography variant="h5" mb={2} fontWeight="bold">
        Admin Dashboard
      </Typography>

      <Grid container spacing={2}>
        <Grid item xs={12} md={3}>
          <StatCard title="Total Users" value={120} icon={<Group />} color="primary" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Lawyers" value={40} icon={<Work />} color="secondary" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Clients" value={70} icon={<Group />} color="info" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Cases" value={95} icon={<Gavel />} color="success" />
        </Grid>
      </Grid>

      <DashboardChart title="System Activity" />
    </Box>
  );
}

// -------------------
// 🧠 Main Component
// -------------------
export default function DashboardPage() {
  const { currentUser, loadingUserInfo } = useAccount();

  if (loadingUserInfo) return <DashboardSkeleton />;
  if (!currentUser) return <Navigate to="/login" replace />;

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      {currentUser.userType === "Client" && <ClientDashboard />}
      {currentUser.userType === "Lawyer" && <LawyerDashboard />}
      {currentUser.userType === "LawyerAdmin" && <LawyerDashboard />}
      {currentUser.userType === "Admin" && <AdminDashboard />}
    </Paper>
  );
}