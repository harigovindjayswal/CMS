import { useQuery } from "@tanstack/react-query";
import agent from "../api/agent";

export const useUtility = (type: string) => {
  const { data, isLoading } = useQuery({
    queryKey: ["optionLoader", type],
    queryFn: async () => {
      const response = await agent.get<OptionLoader[]>(
        `/Utility/GetOptions?type=${type}`
      );
      return response.data;
    },
    enabled: !!type,
  });

  // ✅ Add default option
  const optionLoader: OptionLoader[] = [
    { id: " ", name: "-- Select --" },
    ...(data ?? []),
  ];

  return {
    isLoading,
    optionLoader,
  };
};