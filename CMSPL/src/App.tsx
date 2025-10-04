import { List, ListItem, Typography } from "@mui/material";
import { useEffect, useState } from "react"
import axios from  'axios';
function App() {
  const [clients, setClients]=useState<client[]>([]);
  useEffect(()=>{
    axios.get<client[]>("https://localhost:7242/api/Clients/Client")
    .then(response=>setClients(response.data));
  //  fetch("https://localhost:7242/api/Clients/Client")
  //  .then(Response=>Response.json())
  //  .then(data=>setClients(data));
  },[])
  return (
    <>
      <Typography variant="h3">Welcome to CMS</Typography>
      <List>
        {clients.map((client)=>(
           <ListItem key={client.clientId}>{client.firstName}</ListItem>
        )
        )}
      </List>
    </>
  )
}

export default App
