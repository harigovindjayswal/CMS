import { Box, Typography } from "@mui/material";
import ClientCard from "./ClientCard";
import { useClients } from "../../../lib/hooks/useClients";

export default function ClientList() {
  const { clients, isLoading } = useClients(); 

  if (isLoading) return <Typography>Loading...</Typography>

    if (!clients) return <Typography>No client found</Typography>
  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 3 }}>
      {clients.map((client) => (
        <ClientCard key={client.clientId} 
        clients={client}
        />
      ))}
    </Box>
  );
}
