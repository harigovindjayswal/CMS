import { Box, Container, CssBaseline, Typography } from "@mui/material";
import { useState } from "react";
import NavBar from "./NavBar";
import ClientDashboard from "../../features/clients/dashboards/ClientDashboard";
import { useClients } from "../../lib/hooks/useClients";

function App() {
  const [selectedClient, setSelectedClient] = useState<Client | undefined>(
    undefined
  );
  const [editMode, setEditMode] = useState(false);

  const { clients, isPending } = useClients();

  const handleSelectedClient = (id: number) => {
    setSelectedClient(clients!.find((x) => x.clientId === id));
  };
  const handleCancelClient = () => {
    setSelectedClient(undefined);
  };
  const handleOpenForm = (id?: number) => {
    if (id) handleSelectedClient(id);
    else handleCancelClient();
    setEditMode(true);
  };

  const handleCloseForm = () => {
    setEditMode(false);
  };
  
  return (
    <>
      <Box sx={{ bgcolor: "#eeeeee", minHeight: "100vh" }}>
        <CssBaseline />
        <NavBar openForm={handleOpenForm} />
        <Container maxWidth="xl" sx={{ mt: 3 }}>
          {!clients || isPending ? (
            <Typography>Loading...</Typography>
          ) : (
            <ClientDashboard
              clients={clients}
              handleSelectedClient={handleSelectedClient}
              handleCancelClient={handleCancelClient}
              selectedClient={selectedClient}
              openForm={handleOpenForm}
              closeForm={handleCloseForm}
              editMode={editMode}
            />
          )}
        </Container>
      </Box>
    </>
  );
}

export default App;
