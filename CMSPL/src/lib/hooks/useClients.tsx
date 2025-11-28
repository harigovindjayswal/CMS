import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { useLocation } from "react-router";

export const useClients = (id?:number) => {
  const queryClient = useQueryClient();
  const location=useLocation();
  const { data: clients, isPending } = useQuery({
    queryKey: ["clients"],
    queryFn: async () => {
      const response = await agent.get<Client[]>("/Clients/Client");
      return response.data;
    },
    enabled:!id && location.pathname==='/clients'
  });
  const {data:client,isLoading:isClientLoading}=useQuery({
    queryKey:['clients',id],
    queryFn:async ()=>{
       const response=await agent.get<Client>(`/Clients/Client/${id}`)
       return response.data;
    },
    enabled:!!id
  })
  const updateClient = useMutation({
    mutationFn: async (client: Client) => {
      console.log(client);
      await agent.put("/Clients/Client",client);   
    },
    onSuccess: async ()=>{
       await queryClient.invalidateQueries({
        queryKey:['clients']
       })
    }
  });
  const createClient = useMutation({
    mutationFn: async (client: Client) => {
     const response = await agent.post("/Clients/Client",client);  
     return response.data; 
    },
    onSuccess: async ()=>{
       await queryClient.invalidateQueries({
        queryKey:['clients']
       })
    }
  });
  const deleteClient = useMutation({
    mutationFn: async (id: number) => {
      await agent.delete(`/Clients/Client/${id}`);   
    },
    onSuccess: async ()=>{
       await queryClient.invalidateQueries({
        queryKey:['clients']
       })
    }
  });

  return {
    clients,
    isPending,
    updateClient,
    createClient,
    deleteClient,
    client,
    isClientLoading
  };
};
