import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { useAccount } from "./useAccount";
import { toast } from "react-toastify";

export const useProfile = () => {
  const queryClient = useQueryClient();
  const { currentUser } = useAccount();
  const userType = currentUser?.userType;

  const clientProfileQuery = useQuery({
    queryKey: ["profile", "client"],
    queryFn: async () => {
      const response = await agent.get<ClientProfile>("/Clients/Profile");
      return response.data;
    },
    enabled: userType === "Client",
    retry: false,
  });

  const lawyerProfileQuery = useQuery({
    queryKey: ["profile", "lawyer"],
    queryFn: async () => {
      const response = await agent.get<LawyerProfile>("/Lawyer/Profile");
      return response.data;
    },
    enabled: userType === "Lawyer",
    retry: false,
  });

  const saveClientProfile = useMutation({
    mutationFn: async (profile: ClientProfile) => {
      await agent.put("/Clients/Profile", profile);
    },
    onSuccess: async () => {
      toast.success("Profile saved");
      await queryClient.invalidateQueries({ queryKey: ["profile", "client"] });
    },
  });

  const saveLawyerProfile = useMutation({
    mutationFn: async (profile: LawyerProfile) => {
      await agent.put("/Lawyer/Profile", profile);
    },
    onSuccess: async () => {
      toast.success("Profile saved");
      await queryClient.invalidateQueries({ queryKey: ["profile", "lawyer"] });
    },
  });

  return {
    userType,
    clientProfileQuery,
    lawyerProfileQuery,
    saveClientProfile,
    saveLawyerProfile,
  };
};

