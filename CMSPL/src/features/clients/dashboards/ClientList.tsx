import { Box, Typography } from "@mui/material";
import ClientCard from "./ClientCard";
import { useClients } from "../../../lib/hooks/useClients";

export default function ClientList() {
  const { clients, isPending } = useClients(); 
  if(!clients || isPending) return <Typography>Loading...</Typography>
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
