import { Box } from "@mui/material";
import ClientCard from "./ClientCard";
type Prop = {
  clients: Client[];
  selectClient: (id:number)=>void;
  
};
export default function ClientList({ clients ,selectClient}: Prop) {
  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 3 }}>
      {clients.map((client) => (
        <ClientCard key={client.clientId} 
        clients={client}
        selectClient={selectClient}
        />
      ))}
    </Box>
  );
}
