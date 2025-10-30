import { Box, Container, CssBaseline } from "@mui/material";
import { useEffect, useState } from "react";
import axios from "axios";
import NavBar from "./NavBar";
import ClientDashboard from "../../features/clients/dashboards/ClientDashboard";

function App() {
  const [clients, setClients] = useState<Client[]>([]);
  const [selectedClient, setSelectedClient] = useState<Client | undefined>(
    undefined
  );
  const [editMode, setEditMode] = useState(false);
  useEffect(() => {
    axios
      .get<Client[]>("http://localhost:5000/api/Clients/Client")
      .then((response) => setClients(response.data));
  }, []);

  const handleSelectedClient = (id: number) => {
    setSelectedClient(clients.find((x) => x.clientId === id));
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
  const handleFormSubmit = (client: Client) => {
    if (client.clientId) {
      setClients(
        clients.map((x) => (x.clientId === client.clientId ? client : x))
      );
    } else {
      const newClient = { ...client, clientId: clients.length };
      setSelectedClient(newClient);
      setClients([...clients, newClient]);
    }
    setEditMode(false);
  };

  const handleDelete = (id:number)=>{
      setClients(clients.filter(x=>x.clientId !== id));
  }
  return (
    <>
      <Box sx={{ bgcolor: "#eeeeee" }}>
        <CssBaseline />
        <NavBar openForm={handleOpenForm} />
        <Container maxWidth="xl" sx={{ mt: 3 }}>
          <ClientDashboard
            clients={clients}
            handleSelectedClient={handleSelectedClient}
            handleCancelClient={handleCancelClient}
            selectedClient={selectedClient}
            openForm={handleOpenForm}
            closeForm={handleCloseForm}
            editMode={editMode}
            handleFormSubmit ={handleFormSubmit}
            handleDelete ={handleDelete}
          />
        </Container>
      </Box>
    </>
  );
}

export default App;
