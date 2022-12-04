import { Box, Container, Flex, Heading, Image } from "@chakra-ui/react";
import React, { FunctionComponent } from "react";
import { FreeVisitsView } from "./Pages/FreeVisitsView";

const HomePage: FunctionComponent = () => {
  return (
    <Flex justifyContent="center">
      <Image src="/DentaClinic.png" className="puff-in-center" />
    </Flex>
  );
};

export { HomePage };
