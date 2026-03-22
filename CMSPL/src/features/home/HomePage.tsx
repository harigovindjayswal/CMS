import { Box, Container, Grid, Typography, Button, Paper } from "@mui/material";
import { Link } from "react-router";
import HubIcon from "@mui/icons-material/Hub";

export default function HomePage() {
  return (
    <Box sx={{ bgcolor: "#F5F7FB", minHeight: "100vh" }}>

      {/* 🔷 HEADER */}
      <Box
        sx={{
          px: 4,
          py: 2,
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          bgcolor: "white",
          boxShadow: 1,
        }}
      >
        {/* Logo */}
        <Box display="flex" alignItems="center" gap={1}>
          <HubIcon sx={{ color: "#5E35B1" }} />
          <Typography variant="h6" fontWeight={700}>
            Neurio
          </Typography>
        </Box>

        {/* Nav */}
        <Box display="flex" gap={2}>
          <Button component={Link} to="/login">
            Login
          </Button>
          <Button
            component={Link}
            to="/register"
            variant="contained"
            sx={{ borderRadius: 3 }}
          >
            Get Started
          </Button>
        </Box>
      </Box>

      {/* 🔥 HERO SECTION */}
      <Container maxWidth="lg" sx={{ py: 10 }}>
        <Grid container spacing={6} alignItems="center">

          {/* LEFT */}
          <Grid item xs={12} md={6}>
            <Typography variant="h3" fontWeight={700}>
              Neurio CMS
            </Typography>

            <Typography variant="h5" mt={2} mb={3}>
              Smart Client & Case Management Platform
            </Typography>

            <Typography color="text.secondary" mb={4}>
              Manage clients, lawyer requests, and cases efficiently in one unified system.
            </Typography>

            <Box display="flex" gap={2}>
              <Button
                component={Link}
                to="/register"
                variant="contained"
                size="large"
              >
                Start Free
              </Button>

              <Button
                component={Link}
                to="/login"
                variant="outlined"
                size="large"
              >
                Login
              </Button>
            </Box>
          </Grid>

          {/* RIGHT IMAGE */}
          <Grid item xs={12} md={6}>
            <Box
              component="img"
              src="https://images.unsplash.com/photo-1551434678-e076c223a692"
              sx={{
                width: "100%",
                borderRadius: 4,
              }}
            />
          </Grid>
        </Grid>
      </Container>

      {/* ⚙️ FEATURES */}
      <Container maxWidth="lg" sx={{ py: 8 }}>
        <Typography variant="h4" textAlign="center" mb={6}>
          Powerful Features
        </Typography>

        <Grid container spacing={4}>
          {[
            "Client Management",
            "Case Tracking",
            "Lawyer Requests",
            "Role-Based Access",
            "Secure Authentication",
            "Dashboard Insights",
          ].map((feature) => (
            <Grid item xs={12} md={4} key={feature}>
              <Paper
                sx={{
                  p: 4,
                  borderRadius: 4,
                  height: "100%",
                }}
              >
                <Typography fontWeight={600} mb={1}>
                  {feature}
                </Typography>
                <Typography color="text.secondary">
                  Simplify your workflow with powerful tools.
                </Typography>
              </Paper>
            </Grid>
          ))}
        </Grid>
      </Container>

      {/* 🚀 CTA */}
      <Box textAlign="center" py={10}>
        <Typography variant="h4" mb={3}>
          Ready to get started with Neurio?
        </Typography>

        <Button
          component={Link}
          to="/register"
          variant="contained"
          size="large"
          sx={{ px: 6, py: 1.5, borderRadius: 3 }}
        >
          Create Account
        </Button>
      </Box>

      {/* ⚫ FOOTER */}
      <Box sx={{ bgcolor: "#1A1A1A", color: "white", py: 4 }}>
        <Container maxWidth="lg">
          <Typography variant="h6">Neurio</Typography>
          <Typography variant="body2" color="gray">
            © {new Date().getFullYear()} Neurio. All rights reserved.
          </Typography>
        </Container>
      </Box>
    </Box>
  );
}