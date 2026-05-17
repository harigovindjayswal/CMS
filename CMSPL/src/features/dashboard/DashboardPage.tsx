import { Navigate } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";
import {
  Box,
  Grid,
  Paper,
  Typography,
  Skeleton,
  useTheme,
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
  CartesianGrid,
  Cell,
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
  { name: "May", value: 40 },
  { name: "Jun", value: 23 },
  { name: "Jul", value: 33 },
  { name: "Aug", value: 66 },
  { name: "Sep", value: 80 },
  { name: "Oct", value: 55 },
  { name: "Nov", value: 45 },
  { name: "Dec", value: 65 },
];

// -------------------
// 🔹 Chart Component
// -------------------
function DashboardChart({ title }: ChartProps) {
  const theme = useTheme();

  const currentMonthIndex = new Date().getMonth(); // highlight current month

  return (
    <Paper
      elevation={0}
      tabIndex={0}
      sx={{
        mt: 3,
        p: 3,
        borderRadius: 4,
        border: `1px solid ${theme.palette.divider}`,
        outline: "none",

        // ✅ Soft focus ring (fix for black border)
        "&:focus-visible": {
          boxShadow: `0 0 0 3px ${theme.palette.primary.main}33`,
        },
      }}
    >
      {/* Header */}
      <Box display="flex" justifyContent="space-between" mb={2}>
        <Typography variant="h6" fontWeight={600}>
          {title}
        </Typography>
      </Box>

      {/* Chart */}
      <Box sx={{ outline: "none", "& *:focus": { outline: "none" } }}>
        <ResponsiveContainer width="100%" height={280}>
          <BarChart data={chartData} barSize={24}>
            
            {/* Gradient */}
            <defs>
              <linearGradient id="mainBar" x1="0" y1="0" x2="0" y2="1">
                <stop offset="0%" stopColor={theme.palette.primary.main} />
                <stop offset="100%" stopColor={theme.palette.primary.light} />
              </linearGradient>
            </defs>

            {/* Grid */}
            <CartesianGrid
              strokeDasharray="3 3"
              vertical={false}
              stroke={theme.palette.divider}
            />

            {/* X Axis */}
            <XAxis
              dataKey="name"
              tick={{
                fill: theme.palette.text.secondary,
                fontSize: 12,
              }}
              axisLine={false}
              tickLine={false}
            />

            {/* Y Axis */}
            <YAxis
              tick={{
                fill: theme.palette.text.secondary,
                fontSize: 12,
              }}
              axisLine={false}
              tickLine={false}
            />

            {/* Tooltip */}
            <Tooltip
              cursor={{ fill: "rgba(0,0,0,0.04)" }}
              contentStyle={{
                background: theme.palette.background.paper,
                borderRadius: 10,
                border: `1px solid ${theme.palette.divider}`,
                boxShadow: "0 6px 20px rgba(0,0,0,0.08)",
              }}
            />

            {/* Bars */}
            <Bar
              dataKey="value"
              radius={[8, 8, 0, 0]}
              animationDuration={800}
            >
              {chartData.map((_, index) => (
                <Cell
                  key={`cell-${index}`}
                  fill={
                    index === currentMonthIndex
                      ? theme.palette.primary.main // highlight current month
                      : "url(#mainBar)" // subtle gradient for others
                  }
                  opacity={index === currentMonthIndex ? 1 : 0.6}
                />
              ))}
            </Bar>
          </BarChart>
        </ResponsiveContainer>
      </Box>
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
        Lawyer Requests
      </Typography>

      <Grid container spacing={2}>
        <Grid item xs={12} md={3}>
          <StatCard title="Total" value={12} icon={<Gavel />} color="primary" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Pending" value={3} icon={<PendingActions />} color="warning" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Approved" value={7} icon={<CheckCircle />} color="success" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Rejected" value={2} icon={<Cancel />} color="error" />
        </Grid>
      </Grid>

      <Typography variant="h5" mb={2} fontWeight="bold">
        Cases
      </Typography>

      <Grid container spacing={2}>
        <Grid item xs={12} md={3}>
          <StatCard title="Filed" value={12} icon={<Gavel />} color="primary" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Admitted" value={3} icon={<PendingActions />} color="warning" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Under Hearing" value={7} icon={<CheckCircle />} color="success" />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Reserved for Judgment" value={2} icon={<Cancel />} color="error" />
        </Grid>
      </Grid>

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