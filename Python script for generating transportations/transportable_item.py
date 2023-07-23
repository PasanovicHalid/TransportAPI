class TransportableItem:
    name = ''
    widthRange : list[float]
    heightRange : list[float]
    depthRange : list[float]
    weightRange : list[float]

    def __init__(self, name, widthRange, heightRange, depthRange, weightRange):
        self.name = name
        self.widthRange = widthRange
        self.heightRange = heightRange
        self.depthRange = depthRange
        self.weightRange = weightRange