import { useQuery } from "@tanstack/react-query";
import agent from "../api/agent";

export const useUtility = (type: string, parentId?: number | string) => {
  const { data, isLoading } = useQuery({
    queryKey: ["optionLoader", type, parentId ?? ""],
    queryFn: async () => {
      const query = parentId ? `&parentId=${parentId}` : "";
      const response = await agent.get<OptionLoader[]>(`/Utility/GetOptions?type=${type}${query}`);
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
