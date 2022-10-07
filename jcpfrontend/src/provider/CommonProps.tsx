import React, { useContext, useEffect, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import { JobCodeDTO } from "../DTO/JobCodeDTO";
import { SupplierBranchDTO } from "../DTO/SupplierBranchDTO";
import { SupplierDTO } from "../DTO/SupplierDTO";
import { TechDTO } from "../DTO/TechDTO";

interface CommonProps {
  techs: TechDTO[];
  jobCodes: JobCodeDTO[];
  supplierBranches: SupplierBranchDTO[];
  suppliers: SupplierDTO[];
  quoteStatus: string[];
}

export const CommonPropsContext = React.createContext<CommonProps>(null!);

export function useCommonProps() {
  return useContext(CommonPropsContext);
}

export function CommonPropsProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  const [techs, setTechs] = useState<TechDTO[]>([]);

  const auth = useAuth();

  useEffect(() => {
    auth
      .requestv2("/api/Techs", {
        method: "GET",
      })
      .then((res) => setTechs(res));
  }, []);

  const [jobCodes, setJobCodes] = useState<JobCodeDTO[]>([]);

  useEffect(() => {
    auth
      .requestv2("/api/JobCodes", {
        method: "GET",
      })
      .then((res) => {
        setJobCodes(res);
      });
  }, []);

  const [supplierBranches, setSupplierBranches] = useState<SupplierBranchDTO[]>(
    []
  );

  useEffect(() => {
    auth
      .requestv2("/api/SupplierBranches", {
        method: "GET",
      })
      .then((res) => {
        setSupplierBranches(res);
      });
  }, []);

  const [suppliers, setSuppliers] = useState<SupplierDTO[]>([]);

  useEffect(() => {
    auth
      .requestv2("/api/Suppliers", {
        method: "GET",
      })
      .then((res) => {
        setSuppliers(res);
      });
  }, []);

  const [quoteStatus, setQuoteStatus] = useState<string[]>([]);

  useEffect(() => {
    auth
      .requestv2("/api/QuoteStatus", {
        method: "GET",
      })
      .then((res) => {
        setQuoteStatus(res);
      });
  }, []);

  let Val: CommonProps = {
    techs,
    jobCodes,
    supplierBranches,
    suppliers,
    quoteStatus,
  };

  return (
    <CommonPropsContext.Provider value={Val}>
      {children}
    </CommonPropsContext.Provider>
  );
}
